using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// repairtransfer 的摘要说明
    /// </summary>
    public class repairtransfer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string taskID = context.Request.QueryString["repairID"].ToString();//工单ID
            string teamID = context.Request.QueryString["teamID"].ToString();//受理组ID
            string acceptID = context.Request.QueryString["acceptID"].ToString();//受理人ID

            string userID = context.Request.QueryString["userID"].ToString();//发布人ID


            int flag = 0;
            BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();

            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("TaskId", taskID);
                dic.Add("UpdateType", "Accept");
                dic.Add("Values", teamID+"$"+ userID);
                dic.Add("ChgeUserId", userID);
                flag = repairBLL.ChangeTaskInfo(dic);
            }
            catch (Exception err)
            {
                BLL.Common.Logger.Error("repairTransfer.ashx  Error", err);
            }
            if(flag>0)
            {
                context.Response.Write("success");
            }






          
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