using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HF.Cloud.BLL;
using System.Data;
using System.Web.Script.Serialization;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// repairMessage 的摘要说明
    /// </summary>
    public class repairMessage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            context.Response.ContentType = "text/plain";
            HF.Cloud.BLL.Common.Logger.Error("repairMessage.ashx进来了");
            if (!string.IsNullOrEmpty(context.Request.QueryString["repairID"]) && !string.IsNullOrEmpty(context.Request.QueryString["chatID"]))
            {
                string repairID = context.Request.QueryString["repairID"].ToString();
                string chatID = context.Request.QueryString["chatID"].ToString();
                HF.Cloud.BLL.Common.Logger.Error("repairID:" + repairID + "chatID:" + chatID);
                //获取回复列表
                BLL.RepairTaskBLL repairTask = new BLL.RepairTaskBLL();
                string replyStr = "";
                try
                {
                    //获取回复消息
                    replyStr = repairTask.GetAPPSheetChatListBYChatID(long.Parse(chatID), long.Parse(repairID), 2);
                    BLL.Common.Logger.Error("RepairMessage回复消息：" + replyStr);
                }
                catch (Exception err)
                {
                    BLL.Common.Logger.Error("RepairMessage Error", err);
                }


                List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                json = js.Deserialize<List<Dictionary<string, object>>>(replyStr);

                if (json.Count > 0)
                {
                    string chatID_return = json[json.Count - 1]["ID"].ToString();//保存charID最小值，刷新用 
                    result += "{\"result\":[";
                    for (int i = json.Count - 1; i > -1; i--)//这样可以使新数据在最下面，老数据在上面
                    {
                        string userName = json[i]["UserName"].ToString();
                        string sendTime = json[i]["SendTime"].ToString();
                        string sendDetail = json[i]["SendDetail"].ToString();


                        result += "{\"name\":\"" + userName;
                        result += "\",";
                        result += "\"time\":\"" + sendTime;
                        result += "\",";
                        result += "\"detail\":\"" + sendDetail;
                        result += "\"},";



                    }
                    result = result.Substring(0, result.Length - 1);//确定最后一个逗号
                    result += "],\"chatID\":\"" + chatID_return;
                    result += "\"}";
                }
                else
                {
                    result = "";
                }
                //string dd = "{\"result\":[{\"name\":\"张三\",\"time\":\"2017-2-3\",\"detail\":\"付电话费舒服的书\"},{\"name\":\"李四\",\"time\":\"2012-2-2\",\"detail\":\"还没见过图\"}],\"chatID\":\"232\"}";
                //    result += "{\"result\":[";
                ////用于循环
                //result += "{\"name\":\""+"张三";
                //result += "\",";
                //result += "\"time\":\""+ "2017-2-3";
                //result += "\",";
                //result += "\"detail\":\""+"内容";
                //result += "\"},";


                //result += "],\"chatID\":\""+"123";
                //result += "\"}";

            }
            HF.Cloud.BLL.Common.Logger.Error(result);
            context.Response.Write(result);
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