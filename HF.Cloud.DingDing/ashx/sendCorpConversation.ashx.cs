using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HF.Cloud.DingDing.auth;
using System.Web.Script.Serialization;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// sendCorpConversation 的摘要说明
    /// </summary>
    public class sendCorpConversation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string returnStr = string.Empty;
            context.Response.ContentType = "text/plain";
            string touserStr = context.Request["touser"].ToString();//员工列表id
            string topartyStr = context.Request["toparty"].ToString();//部门列表Id
            string agentidStr = context.Request["agentid"].ToString();//企业应用id
            string codeStr = context.Request["code"].ToString();//临时授权码
            string msgtypeStr = context.Request["msgtype"].ToString();//消息类型
            string contentStr = context.Request["content"].ToString();//消息内容
            string message_urlStr = context.Request["message_url"].ToString();//消息链接

            string customerStr = context.Request["customer"].ToString();//客户名称
            string sheetTypeStr = context.Request["sheetType"].ToString();//工单类型


            //{
            //    "msgtype": "oa",
            //     "oa": {
            //        "message_url": "http://dingtalk.com",
            //               "head": {
            //                     "bgcolor": "FFBBBBBB",
            //                        "text": "新工单"
            //                       },
            //               "body": {
            //                     "title": "正文标题",
            //"form": [
            //        {
            //            "key": "姓名:",
            //            "value": "张三"
            //        },
            //        {
            //            "key": "年龄:",
            //            "value": "20"
            //        }
            //      ]

            //                         }
            //    }
            //}


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("touser", touserStr);
            dic.Add("toparty", topartyStr);
            dic.Add("agentid", agentidStr);
            dic.Add("code", codeStr);
            dic.Add("msgtype", msgtypeStr);

            Dictionary<string, object> dic_oa = new Dictionary<string, object>();
            dic_oa.Add("message_url", message_urlStr);
                    Dictionary<string, object> dic_head = new Dictionary<string, object>();
                    dic_head.Add("bgcolor", "3333ff");
                    dic_head.Add("text", "新工单！");
            dic_oa.Add("head", dic_head);
                    Dictionary<string, object> dic_body = new Dictionary<string, object>();
                    dic_body.Add("title", contentStr);
                        List<Dictionary<string, object>> dic_form = new List<Dictionary<string, object>>();
                           Dictionary<string, object> dic_person01 = new Dictionary<string, object>();
                           dic_person01.Add("key", "客户：");
                           dic_person01.Add("value", customerStr);
                           Dictionary<string, object> dic_person02 = new Dictionary<string, object>();
                           dic_person02.Add("key", "工单类型：");
                           dic_person02.Add("value", sheetTypeStr);
                       dic_form.Add(dic_person01);
                       dic_form.Add(dic_person02);
                   dic_body.Add("form", dic_form);
            dic_oa.Add("body", dic_body);
            dic.Add("oa", dic_oa);

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            string json = js.Serialize(dic);


            HF.Cloud.BLL.Common.Logger.Error(json);
            //获取token
            string accessToken = AuthHelper.getAccessToken();
            HF.Cloud.BLL.Common.Logger.Error("accessToken:" + accessToken);
            //String url_Conversation = Env.OAPI_HOST + "/message/send?" +
            //        "access_token=" + accessToken;
            //https://oapi.dingtalk.com/message/sendByCode?access_token=ACCESS_TOKEN
            String url_Conversation = Env.OAPI_HOST + "/message/sendByCode?" +
                    "access_token=" + accessToken;


            string conversationString = AuthHelper.Post(url_Conversation, json);  //获取token的字符串

            HF.Cloud.BLL.Common.Logger.Error("conversationString:" + conversationString);
            Dictionary<string, object> json_Conversation = new Dictionary<string, object>();
            JavaScriptSerializer js_Conversation = new JavaScriptSerializer();
            js_Conversation.MaxJsonLength = int.MaxValue;
            json_Conversation = js_Conversation.Deserialize<Dictionary<string, object>>(conversationString);
            if (json_Conversation.Keys.Contains("errmsg"))
            {
                if (json_Conversation["errmsg"].ToString() == "ok")
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