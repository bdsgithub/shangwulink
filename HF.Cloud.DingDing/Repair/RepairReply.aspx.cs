using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.BLL;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace HF.Cloud.DingDing.Repair
{
    public partial class RepairReply : Base.BasePage
    {
        public StringBuilder htmlStr = new StringBuilder();

        public string repairIDStr = string.Empty;//ID
        public string typeNameStr = string.Empty;//设备类型
        public string taskStatusStr = string.Empty;//所处阶段，处理？完成？

        public string realseNameStr = string.Empty;//工单创建人
        public string writeTimeStr = string.Empty;//创建时间
        public string useTimeStr = string.Empty;//已耗时
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RepairID"]))
                {
                    string repairID = Request.QueryString["RepairID"].ToString();
                    repairIDStr = repairID;
                    BLL.Common.Logger.Error("repairID" + repairID);

                    hf_repairID.Value = repairID;
                    hf_mainID.Value = MainID.ToString();
                    hf_sendID.Value = UserID.ToString();
                    hf_sendTime.Value = DateTime.Now.ToString();

                    //获取当前维修单信息
                    BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
                    List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();

                    try
                    {
                        json = repairBLL.GetTaskDetails(long.Parse(repairID));
                    }
                    catch (Exception err)
                    {
                        BLL.Common.Logger.Error("RepairReply  Error", err);
                    }
                    BLL.Common.Logger.Error("json.Count:" + json.Count);
                    if (json.Count > 0)
                    {
                        typeNameStr = json[0]["TypeName"].ToString();
                        taskStatusStr = json[0]["taskStatus"].ToString();

                        realseNameStr = json[0]["realseName"].ToString(); 
                        writeTimeStr = json[0]["WriteTime"].ToString();
                        useTimeStr = json[0]["UseTime"].ToString(); 
                    }

                    //获取回复列表
                    BLL.RepairTaskBLL repairTask = new BLL.RepairTaskBLL();
                    string replyStr = "";
                    try
                    {
                        //获取回复消息
                        replyStr = repairTask.GetAPPSheetChatListBYChatID(0, long.Parse(repairID), 2);
                        BLL.Common.Logger.Error("RepairReply回复消息："+replyStr);
                    }
                    catch (Exception err)
                    {
                        BLL.Common.Logger.Error("RepairReply Error", err);
                    }

                    List<Dictionary<string, object>> json_Reply = new List<Dictionary<string, object>>();
                    JavaScriptSerializer js_Reply = new JavaScriptSerializer();
                    js_Reply.MaxJsonLength = int.MaxValue;
                    json_Reply = js_Reply.Deserialize<List<Dictionary<string, object>>>(replyStr);

                   
                    hf_chatID.Value = json_Reply[json_Reply.Count - 1]["ID"].ToString();//保存charID最小值，刷新用 
                    hf_userName.Value = UserName;   //当前用户名字         
                    for (int i = json_Reply.Count - 1; i > -1; i--)
                    {
                        //HF.Cloud.BLL.Common.Logger.Error("---" + i);
                        //BLL.Common.Logger.Error("回复类型：" + json[i]["MessageType"].ToString());
                        string userName = json_Reply[i]["UserName"].ToString();
                        string sendTime = json_Reply[i]["SendTime"].ToString();
                        string sendDetail = json_Reply[i]["SendDetail"].ToString();
                        if (json_Reply[i]["MessageType"].ToString() == "Character")//如果是文字的话
                        {
                            if (userName == UserName)
                            {
                                //htmlStr.Append("<div style='background-color:#CCFFFF'>" + userName + " - " + sendTime + "</div>");
                                //htmlStr.Append("<div>" + sendDetail + "</div>");
                                //htmlStr.Append("<div>---</div>");

                                htmlStr.Append("<div class=\"SheetReply_bigDIV\">");
                                htmlStr.Append("<div class=\"SheetReply_replyTitle\">");
                                htmlStr.Append("<div class=\"SheetReply_imgDIV_right\">");
                                htmlStr.Append("<img src=\"../img/z.png\"/>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("<div class=\"SheetReply_nameDIV_right\">");
                                htmlStr.Append("<P>" + userName + "</P>");
                                htmlStr.Append("<P class=\"SheetReply_timeDIV\">" + sendTime + "</P>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("<div class=\"SheetReply_replyDIV_right\">");
                                htmlStr.Append("<p>" + sendDetail + "</p>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("</div>");



                            }
                            else
                            {
                                //htmlStr.Append("<div>" + userName + " - " + sendTime + "</div>");
                                //htmlStr.Append("<div>" + sendDetail + "</div>");
                                //htmlStr.Append("<div>---</div>");
                                htmlStr.Append("<div class=\"SheetReply_bigDIV\">");
                                htmlStr.Append("<div class=\"SheetReply_replyTitle\">");
                                htmlStr.Append("<div class=\"SheetReply_imgDIV\">");
                                htmlStr.Append("<img src=\"../img/z.png\"/>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("<div class=\"SheetReply_nameDIV\">");
                                htmlStr.Append("<P>" + userName + "</P>");
                                htmlStr.Append("<P class=\"SheetReply_timeDIV\">" + sendTime + "</P>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("<div class=\"SheetReply_replyDIV\">");
                                htmlStr.Append("<p>" + sendDetail + "</p>");
                                htmlStr.Append("</div>");
                                htmlStr.Append("</div>");
                            }
                        }
                        if (json_Reply[i]["MessageType"].ToString() == "PIC")//如果是图片
                        {
                            if (userName == UserName)
                            {
                                htmlStr.Append("<div style='background-color:#CCFFFF'>" + userName + " - " + sendTime + "</div>");
                                htmlStr.Append("<div>" + sendDetail + "</div>");
                                htmlStr.Append("<div>---</div>");
                            }
                            else
                            {
                                htmlStr.Append("<div>" + userName + " - " + sendTime + "</div>");
                                htmlStr.Append("<div>" + sendDetail + "</div>");
                                htmlStr.Append("<div>---</div>");
                            }
                        }
                    }






                }
            }








        }
    }
}