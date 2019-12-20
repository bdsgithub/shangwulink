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

namespace HF.Cloud.DingDing.Sheet
{
    public partial class SheetReply : Base.BasePage
    {
        public StringBuilder htmlStr = new StringBuilder();
        public string sheetIDStr = string.Empty;//ID
        public string sheetTypeStr = string.Empty;//类型
        public string sheetStateStr = string.Empty;//所处阶段，处理？完成？

        public string writerNameStr = string.Empty;//工单创建人
        public string writeTimeStr = string.Empty;//创建时间

        protected void Page_Load(object sender, EventArgs e)
        {
            string sheetID = string.Empty;
            if (!IsPostBack)
            {

                if (!String.IsNullOrEmpty(Request.QueryString["sheetID"]))
                {
                    sheetID = Request.QueryString["sheetID"].ToString();
                }
                hf_sheetID.Value = sheetID;
                hf_mainID.Value = MainID.ToString();
                hf_sendID.Value = UserID.ToString();
                hf_sendTime.Value = DateTime.Now.ToString();
                //获取当前工单
                SheetBL shBLL = new SheetBL();
                DataTable dt = new DataTable();
                dt = shBLL.GetAPPSheetBYID(long.Parse(sheetID));

                sheetIDStr = sheetID;
                sheetTypeStr = dt.Rows[0]["TypeName"].ToString();
                sheetStateStr = dt.Rows[0]["SheetStateCN"].ToString();

                writerNameStr = dt.Rows[0]["WriterName"].ToString();
                writeTimeStr = dt.Rows[0]["WriteTime"].ToString();







                //获取回复列表
                SheetBL bl = new SheetBL();

                string replyStr = string.Empty;
                if (String.IsNullOrEmpty(Request.QueryString["chatID"]))
                {
                    replyStr = bl.GetAPPSheetChatListBYChatID(0, long.Parse(sheetID));
                }
                else
                {
                    string chatIDStr = Request.QueryString["chatID"];
                    replyStr = bl.GetAPPSheetChatListBYChatID(long.Parse(chatIDStr), long.Parse(sheetID));
                }


                List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;
                json = js.Deserialize<List<Dictionary<string, object>>>(replyStr);
                //if (json.Count < 1)
                //{
                //    //显示已全部加载
                //    string js_loading = "<script language=javascript>dd.ready(function () {dd.device.notification.toast({ text: '已全部加载...' }); });</script>";
                //    HttpContext.Current.Response.Write(js_loading);
                //}
                //else
                //{
                    hf_chatID.Value = json[json.Count - 1]["ID"].ToString();//保存charID最小值，刷新用 
                    hf_userName.Value = UserName;   //当前用户名字                                                 //for(int i=0;i<json.Count;i++)
                    for (int i = json.Count - 1; i > -1; i--)
                    {
                        //HF.Cloud.BLL.Common.Logger.Error("---" + i);
                        //BLL.Common.Logger.Error("回复类型：" + json[i]["MessageType"].ToString());
                        string userName = json[i]["UserName"].ToString();
                        string sendTime = json[i]["SendTime"].ToString();
                        string sendDetail = json[i]["SendDetail"].ToString();
                        if (json[i]["MessageType"].ToString() == "Character")//如果是文字的话
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
                        if (json[i]["MessageType"].ToString() == "PIC")//如果是图片
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
                //}
            }




        }
    }
}