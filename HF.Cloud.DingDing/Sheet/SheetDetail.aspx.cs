using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HF.Cloud.BLL;
using System.Data;
using System.Web.Script.Serialization;

namespace HF.Cloud.DingDing.Sheet
{
    public partial class SheetDetail : Base.BasePage
    {
        public string sheetIDStr = string.Empty;//ID
        public string sheetTypeStr = string.Empty;//类型
        public string sheetStateStr = string.Empty;//所处阶段，处理？完成？

        public string writerNameStr = string.Empty;//工单创建人
        public string writeTimeStr = string.Empty;//创建时间
         
        public string clientNameStr = string.Empty;//客户
        public string clientAdrStr = string.Empty;//客户地址
        public string contactStr = string.Empty;//客户联系人
        public string dianhuaStr = string.Empty;//联系人电话

        public string userNameStr = string.Empty;//受理人
        public string sheetDetailStr = string.Empty;//描述
        public string sheetSummaryStr = string.Empty;//工单总结
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["sheetID"]))
                {
                    string sheetID = Request.QueryString["sheetID"].ToString();
                    hf_sheetID.Value = sheetID;
                    //hf_mainID.Value = MainID.ToString();
                    //hf_sendID.Value = UserID.ToString();
                    //hf_sendTime.Value = DateTime.Now.ToString();
                    SheetBL shBLL = new SheetBL();
                    DataTable dt = new DataTable();
                    dt = shBLL.GetAPPSheetBYID(long.Parse(sheetID));
                 
                    sheetIDStr = sheetID;
                    sheetTypeStr = dt.Rows[0]["TypeName"].ToString();
                    sheetStateStr = dt.Rows[0]["SheetStateCN"].ToString();

                    writerNameStr = dt.Rows[0]["WriterName"].ToString();
                    writeTimeStr = dt.Rows[0]["WriteTime"].ToString();

                    clientNameStr = dt.Rows[0]["ClientName"].ToString();
                    clientAdrStr = dt.Rows[0]["ClientAdr"].ToString();
                    contactStr = dt.Rows[0]["Contact"].ToString();
                    dianhuaStr = dt.Rows[0]["dianhua"].ToString();

                    userNameStr = dt.Rows[0]["UserName"].ToString();
                   
                    sheetDetailStr = dt.Rows[0]["SheetDetail"].ToString();
                    sheetSummaryStr= dt.Rows[0]["SheetSummary"].ToString();




                    hf_userNameStr.Value = userNameStr;//受理人
                    hf_userName.Value = UserName;//当前用户名字
                    hf_sheetStateStr.Value = sheetStateStr;//工单状态
                    

                    ////获取回复列表
                    //SheetBL bl = new SheetBL();

                    //string replyStr = string.Empty;
                    //if (String.IsNullOrEmpty(Request.QueryString["chatID"]))
                    //{
                    //    replyStr = bl.GetAPPSheetChatListBYChatID(0, long.Parse(sheetID));
                    //}
                    //else
                    //{
                    //    string chatIDStr = Request.QueryString["chatID"];
                    //    replyStr = bl.GetAPPSheetChatListBYChatID(long.Parse(chatIDStr), long.Parse(sheetID));
                    //}


                    //List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //js.MaxJsonLength = int.MaxValue;
                    //json = js.Deserialize<List<Dictionary<string, object>>>(replyStr);
                    //if (json.Count < 1)
                    //{
                    //    //显示已全部加载
                    //    string js_loading = "<script language=javascript>dd.ready(function () {dd.device.notification.toast({ text: '已全部加载...' }); });</script>";
                    //    HttpContext.Current.Response.Write(js_loading);
                    //}
                    //else
                    //{
                    //    hf_chatID.Value = json[json.Count - 1]["ID"].ToString();//保存charID最小值，刷新用 
                    //                                                            //for(int i=0;i<json.Count;i++)
                    //    for (int i = json.Count - 1; i > -1; i--)
                    //    {
                    //        HF.Cloud.BLL.Common.Logger.Error("---" + i);
                    //        BLL.Common.Logger.Error("回复类型：" + json[i]["MessageType"].ToString());
                    //        string userName = json[i]["UserName"].ToString();
                    //        string sendTime = json[i]["SendTime"].ToString();
                    //        string sendDetail = json[i]["SendDetail"].ToString();
                    //        if (json[i]["MessageType"].ToString() == "Character")//如果是文字的话
                    //        {
                    //            if (userName == UserName)
                    //            {
                    //                htmlStr.Append("<div style='background-color:#CCFFFF'>" + userName + " - " + sendTime + "</div>");
                    //                htmlStr.Append("<div>" + sendDetail + "</div>");
                    //                htmlStr.Append("<div>---</div>");
                    //            }
                    //            else
                    //            {
                    //                htmlStr.Append("<div>" + userName + " - " + sendTime + "</div>");
                    //                htmlStr.Append("<div>" + sendDetail + "</div>");
                    //                htmlStr.Append("<div>---</div>");
                    //            }
                    //        }
                    //        if (json[i]["MessageType"].ToString() == "PIC")//如果是图片
                    //        {
                    //            if (userName == UserName)
                    //            {
                    //                htmlStr.Append("<div style='background-color:#CCFFFF'>" + userName + " - " + sendTime + "</div>");
                    //                htmlStr.Append("<div>" + sendDetail + "</div>");
                    //                htmlStr.Append("<div>---</div>");
                    //            }
                    //            else
                    //            {
                    //                htmlStr.Append("<div>" + userName + " - " + sendTime + "</div>");
                    //                htmlStr.Append("<div>" + sendDetail + "</div>");
                    //                htmlStr.Append("<div>---</div>");
                    //            }
                    //        }
                    //    }
                    //}





                }
            }




        }

        protected void lnkBtnSendSheetState_Click(object sender, EventArgs e)
        {
            string sheetState = hf_sheetStateStr.Value;//工单状态
            BLL.Common.Logger.Error("点击按钮，工单状态为：" + sheetState);
            if(sheetState== "已受理")
            {
                //修改状态为处理
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ID", hf_sheetID);//工单主键
                dic.Add("MainID", MainID);//服务商主键
                dic.Add("UserID", UserID);//操作人主键
                dic.Add("UpdateDime", DateTime.Now.ToString("s"));//修改时间
                dic.Add("UpdateType", "UpdateState");//修改类型
                dic.Add("UpdateDetail", "3");//修改的内容  “3”为处理中
                
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = int.MaxValue;

                string json = js.Serialize(dic);
                
                BLL.SheetBL sheetBL = new SheetBL();
                long result=sheetBL.UpdateAPPSheet(json);
                BLL.Common.Logger.Error("点击处理后返回数据result，大于0则执行成功：" + result);
                if (result>0)
                {
                    Response.Write("<script>window.reload();</script>");
                }
            }
            if (sheetState == "处理中")
            {
                //修改状态为完成

            }

        }
    }
}