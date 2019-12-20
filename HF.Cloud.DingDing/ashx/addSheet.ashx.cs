using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// addSheet 的摘要说明
    /// </summary>
    public class addSheet : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string returnStr = string.Empty;
            context.Response.ContentType = "text/plain";
            
            string mainIDStr = context.Request["MainID"].ToString();
            string clientIDStr = context.Request["ClientID"].ToString();
            string writeIDStr = context.Request["WriteID"].ToString();
            string sheetTypeStr = context.Request["SheetType"].ToString();
            string sheetDetailStr = context.Request["SheetDetail"].ToString();
            string teamIDStr = context.Request["TeamID"].ToString();
            string acceptIDStr = context.Request["AcceptID"].ToString();
            string linkNameStr = context.Request["LinkName"].ToString();
            string linkTelStr = context.Request["LinkTel"].ToString();
            HF.Cloud.BLL.Common.Logger.Error("-mainIDStr:" + mainIDStr +
                "-clientIDStr:" + clientIDStr + "-writeIDStr:" + writeIDStr +
                "-sheetTypeStr:" + sheetTypeStr + "-sheetDetailStr:" + sheetDetailStr +
                "-teamIDStr:" + teamIDStr + "-acceptIDStr:" + acceptIDStr +
                "-linkNameStr:" + linkNameStr
                + "-linkTelStr:" + linkTelStr);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("MainID", mainIDStr);
            dic.Add("ClientID", clientIDStr);
            dic.Add("WriteID", writeIDStr);
            dic.Add("SheetType", sheetTypeStr);
            dic.Add("SheetDetail", sheetDetailStr);
            dic.Add("TeamID", teamIDStr);
            dic.Add("AcceptID", acceptIDStr);
            dic.Add("LinkName", linkNameStr);
            dic.Add("LinkTel", linkTelStr);

            dic.Add("ContactID", "");
            dic.Add("WriteTime", DateTime.Now.ToString());
            dic.Add("WriteAdr", "");
            dic.Add("SheetTitle", "");
            dic.Add("SheetPriority", "3");
            dic.Add("SheetState", "2");
            dic.Add("FollowID", "");
            dic.Add("AssetID", "0");
            dic.Add("DataType", "1");//消息数据类型


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            string json = js.Serialize(dic);
            
            long sheet_ReplyID = 0; 
            SheetBL bl = new SheetBL();
            try {
                sheet_ReplyID = bl.InsertAPPNewSheet(json);
            }
            catch(Exception ex)
            {
               HF.Cloud.BLL.Common.Logger.Error("addSheet Error", ex);
            }
            
            if(sheet_ReplyID > 0)
            {
                returnStr = sheet_ReplyID.ToString();
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