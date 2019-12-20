using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// addRepair 的摘要说明
    /// </summary>
    public class addRepair : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string mainIDStr = context.Request["MainID"].ToString();
            string clientIDStr = context.Request["ClientID"].ToString();
            string taskDetailStr = context.Request["TaskDetail"].ToString();
            string taskPriorityStr = "3";
         
            string linkNameStr = context.Request["LinkName"].ToString();
            string linkTelStr = context.Request["LinkTel"].ToString();
            string assetTypeIDStr = context.Request["AssetTypeID"].ToString();
            string writeIDStr = context.Request["WriteID"].ToString();
            string writeAdrStr = "";
         
            string repaireSummaryStr ="";
            string teamIDStr = context.Request["TeamID"].ToString();
            string acceptIDStr = context.Request["AcceptID"].ToString();
          
            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MainID", mainIDStr);
            dic.Add("ClientID", clientIDStr);
            dic.Add("TaskDetail", taskDetailStr);
            dic.Add("TaskPriority", taskPriorityStr);

            dic.Add("LinkName", linkNameStr);
            dic.Add("LinkTel", linkTelStr);
            dic.Add("AssetTypeID", assetTypeIDStr);
            dic.Add("WriteID", writeIDStr);
            dic.Add("WriteAdr", writeAdrStr);

            dic.Add("RepaireSummary", repaireSummaryStr);
            dic.Add("TeamID", teamIDStr);
            dic.Add("AcceptID", acceptIDStr);

            dic.Add("AssetID", string.Empty);

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            string json = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("addRepair提交的数据json：" + json);

            long tid = 0;
            try
            {
                BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
                tid = repairBLL.AddTask(dic);
            }
            catch (Exception err)
            {
                HF.Cloud.BLL.Common.Logger.Error("addRepair.ashx Error", err);
            }
            if(tid>0)
            {
                context.Response.Write(tid);
                // 发送钉钉会话消息
                sendDingSheetMessage(tid, long.Parse(acceptIDStr), long.Parse(clientIDStr), long.Parse(assetTypeIDStr));
            }

         











        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




        /// <summary>
        /// 向钉钉发送维修单会话消息
        /// </summary>
        /// <param name="repairID">维修单id</param>
        /// <param name="acceptID">受理人，发送人id</param>
        /// <param name="clientID">客户id</param>
        /// <param name="assetTypeID">设备类型ID</param>
        public  void sendDingSheetMessage(long repairID, long acceptID, long clientID, long assetTypeID)
        {
            //发送钉钉会话消息--- 0维修单ID，1受理人，2客户，3设备类型

            //通过受理人id获取受理人的DingID
            string DingID = string.Empty;
            //通过受理人id查询用户表，获取受理人信息
            Model.SB_UserEL user_Ding = new Model.SB_UserEL();
            if (acceptID > 0)
            {
                user_Ding.ID = acceptID;//受理人的id
            }
            user_Ding.ExecuteEL(4);//Select By ID
            if (user_Ding != null && user_Ding.DingID != null)
            {
                DingID = user_Ding.DingID.ToString();//获取到DingID
                BLL.Common.Logger.Error("获取到受理人DingID：" + DingID);
            }
            else//如果没有DingID则返回不再继续执行
            {
                BLL.Common.Logger.Error("当前受理人：" + user_Ding.UserName + "-DingID为空！");
                return;
            }

            //通过客户id获取客户名称
            string clienName_Ding = string.Empty;
            Model.C_ClientEL client_Ding = new Model.C_ClientEL();
            client_Ding.ID = clientID;//客户id
            client_Ding.ExecuteEL(4);//Select By ID
            if (client_Ding != null)
            {
                clienName_Ding = client_Ding.ClientName;//获取到ClientName
                BLL.Common.Logger.Error("获取到客户ClientName：" + clienName_Ding);
            }
            //通过设备类型ID获取设备类型名称
            string assetTypeName_Ding = string.Empty;
            Model.A_AssetTypeEL assetType_Ding = new Model.A_AssetTypeEL();
            assetType_Ding.ID = assetTypeID;//设备类型ID
            assetType_Ding.ExecuteEL(4);//Select By ID
            if (assetType_Ding != null)
            {
                assetTypeName_Ding = assetType_Ding.TypeName;//取得sheetTypeName_Ding
                BLL.Common.Logger.Error("获取到设备类型名称assetTypeName_Ding：" + assetTypeName_Ding);
            }



            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("touser", DingID);//员工受理人id，也就是要发送钉钉消息的对象
            dic.Add("toparty", "");//部门列表
            dic.Add("agentid", BLL.DingBL.agentId);//微应用的id
            dic.Add("msgtype", "oa");//消息类型  oa消息

            Dictionary<string, object> dic_oa = new Dictionary<string, object>();
            dic_oa.Add("message_url",
                "http://192.168.1.42:8123/Repair/RepairDetail.aspx?dd_nav_bgcolor=FF5E97F6&repairID="//地址为钉钉易维客微应用的地址
                + repairID);//
            Dictionary<string, object> dic_head = new Dictionary<string, object>();
            dic_head.Add("bgcolor", "3333ff");
            dic_head.Add("text", "新工单！");
            dic_oa.Add("head", dic_head);
            Dictionary<string, object> dic_body = new Dictionary<string, object>();
            dic_body.Add("title", "您有新的维修单，请注意查收！");
            List<Dictionary<string, object>> dic_form = new List<Dictionary<string, object>>();
            Dictionary<string, object> dic_person01 = new Dictionary<string, object>();
            dic_person01.Add("key", "客户：");
            dic_person01.Add("value", clienName_Ding);
            Dictionary<string, object> dic_person02 = new Dictionary<string, object>();
            dic_person02.Add("key", "设备类型：");
            dic_person02.Add("value", assetTypeName_Ding);
            dic_form.Add(dic_person01);
            dic_form.Add(dic_person02);
            dic_body.Add("form", dic_form);
            dic_oa.Add("body", dic_body);
            dic.Add("oa", dic_oa);

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            string json = js.Serialize(dic);


            HF.Cloud.BLL.Common.Logger.Error("钉钉json会话消息：" + json);
            //获取token
            string accessToken = BLL.DingBL.getAccessToken();
            HF.Cloud.BLL.Common.Logger.Error("accessToken:" + accessToken);
            String url_Conversation = BLL.DingBL.api_host + "/message/send?" +
                    "access_token=" + accessToken;
            //https://oapi.dingtalk.com/message/sendByCode?access_token=ACCESS_TOKEN
            //String url_Conversation = Env.OAPI_HOST + "/message/sendByCode?" +
            //        "access_token=" + accessToken;

            //post提交url和json数据
            string conversationString = BLL.DingBL.Post(url_Conversation, json);

            HF.Cloud.BLL.Common.Logger.Error("conversationString:" + conversationString);
            Dictionary<string, object> json_Conversation = new Dictionary<string, object>();
            JavaScriptSerializer js_Conversation = new JavaScriptSerializer();
            js_Conversation.MaxJsonLength = int.MaxValue;
            json_Conversation = js_Conversation.Deserialize<Dictionary<string, object>>(conversationString);
            if (json_Conversation.Keys.Contains("errmsg"))
            {
                if (json_Conversation["errmsg"].ToString() == "ok")
                {
                    BLL.Common.Logger.Error("受理人:" + user_Ding.UserName + "发送会话消息成功！");
                }
            }

        }













    }
}