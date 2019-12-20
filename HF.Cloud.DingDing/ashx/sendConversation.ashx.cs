using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HF.Cloud.DingDing.auth;
using System.Web.Script.Serialization;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// sendConversation 的摘要说明
    /// </summary>
    public class sendConversation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string returnStr = string.Empty;
            context.Response.ContentType = "text/plain";
            string senderStr = context.Request["sender"].ToString();//钉钉发送人id
            string cidStr = context.Request["cid"].ToString();//群消息或者个人聊天会话Id
            string msgtypeStr = context.Request["msgtype"].ToString();//消息类型
            string contentStr = context.Request["content"].ToString();//消息主题


            string data = "{\"sender\":\"" + senderStr
                + "\",\"cid\":\"" + cidStr
                + "\",\"msgtype\":\"" + msgtypeStr
                + "\",\"text\":{\"content\":\"" + contentStr + "\"}}";


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("sender", senderStr);
            dic.Add("cid", cidStr);
            dic.Add("msgtype", msgtypeStr);

            Dictionary<string, object> dic_content = new Dictionary<string, object>();
            dic_content.Add("content", contentStr);

            dic.Add("text", dic_content);

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            string json = js.Serialize(dic);


            HF.Cloud.BLL.Common.Logger.Error(json);
          
            //获取token
            string accessToken = AuthHelper.getAccessToken();
            HF.Cloud.BLL.Common.Logger.Error("accessToken:" + accessToken);
            String url_Conversation = Env.OAPI_HOST + "/message/send_to_conversation?" +
                    "access_token=" + accessToken;

            string conversationString = AuthHelper.Post(url_Conversation, json);  //获取token的字符串

            HF.Cloud.BLL.Common.Logger.Error("conversationString:" + conversationString);
            Dictionary<string, object> json_Conversation = new Dictionary<string, object>();
            JavaScriptSerializer js_Conversation = new JavaScriptSerializer();
            js_Conversation.MaxJsonLength = int.MaxValue;
            json_Conversation = js_Conversation.Deserialize<Dictionary<string, object>>(conversationString);
            if (json_Conversation.Keys.Contains("errmsg"))
            {
                if(json_Conversation["errmsg"].ToString()=="ok")
                {
                    returnStr = "success";
                }
            }
            
            context.Response.Write(returnStr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}