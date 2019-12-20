using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// sheetReply 的摘要说明
    /// </summary>
    public class sheetReply : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取消息类型文字，图片，声音等
            string replyType = context.Request.QueryString["type"];
            string returnStr = string.Empty;
            switch (replyType)
            {
                case "Character":
                    {
                        //returnStr = GetPatrolRecord(long.Parse(context.Request.QueryString["id"].ToString()));
                        string jsonStr = GetReplyResult(context);
                        SheetBL bl = new SheetBL();
                        long sqlResult = bl.InsertAPPChat(jsonStr);
                        BLL.Common.Logger.Error("操作数据库返回的值:" + sqlResult);
                        if (sqlResult > 0)
                        {
                            returnStr = "success";
                        }
                    }
                    break;
                case "PIC":
                    { returnStr = ""; }
                    break;


            }

            context.Response.Write(returnStr);
        }
        private string GetReplyResult(HttpContext context)
        {
            string sheetID = context.Request.QueryString["sheetID"];
            string mainID = context.Request.QueryString["mainID"];
            string sendID = context.Request.QueryString["sendID"];
            string sendTime = context.Request.QueryString["sendTime"];
            string detail = context.Request.QueryString["detail"];

            BLL.Common.Logger.Error("sheetID:" + sheetID + "-mainID:" + mainID + "-sendID:" + sendID
                + "-sendTime:" + sendTime + "-detail:" + detail);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MainID", mainID);
            dic.Add("SheetID", sheetID);
            dic.Add("SendID", sendID);
            dic.Add("SendTime", sendTime);
            dic.Add("Type", "Character");
            dic.Add("FileLength", 0);
            dic.Add("Detail", detail);


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            string json = js.Serialize(dic);

            return json;
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