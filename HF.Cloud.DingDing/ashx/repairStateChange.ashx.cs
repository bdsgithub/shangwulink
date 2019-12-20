using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// repairStateChange 的摘要说明
    /// </summary>
    public class repairStateChange : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //TaskId 维修单Id   长整形
            //TaskState   维修单状态 整形
            //UpdateWriter 更新人 长整形
            //WriteAdr    更新地址 字符
            // repairID = ' + repairID + ' & taskState = 5 & updateWriter = ' + updateWriter + ' & writeAdr = ZhanWeiFu',
    
            string taskID = context.Request.QueryString["repairID"].ToString();
            string taskState = context.Request.QueryString["taskState"].ToString();
            string updateWriter = context.Request.QueryString["updateWriter"].ToString();
            string writeAdr = context.Request.QueryString["writeAdr"].ToString();
            BLL.Common.Logger.Error("taskID:" + taskID+
                "--taskState:" + taskState + "--updateWriter:" + updateWriter +
                "--writeAdr:" + writeAdr);

            int flag = 0;
            BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("TaskId", taskID);
                dic.Add("TaskState", taskState);
                dic.Add("UpdateWriter", updateWriter);
                dic.Add("WriteAdr", writeAdr);
                flag = repairBLL.ChangeTaskStatus(dic);
                BLL.Common.Logger.Error("repairBLL.ChangeTaskStatus返回值："+flag);
            }
            catch (Exception err)
            {
                BLL.Common.Logger.Error("RepairStateChange.ashx  Error", err);
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