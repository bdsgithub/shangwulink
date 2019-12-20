using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HF.Cloud.BLL;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;//引用Newtonsoft.Json

namespace HF.Cloud.DingDing.Sheet
{
    public partial class SheetList : Base.BasePage
    {
        public StringBuilder htmlstr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["SheetType"]))
                {
                    string sheetType = Request.QueryString["SheetType"].ToString();
                    SheetBL blSheet = new SheetBL();
                    //获取单单列表
                    string sheetTypes = blSheet.GetAPPSheetList(MainID, UserID, sheetType, null);
                    HF.Cloud.BLL.Common.Logger.Error("sheetTypes------返回的sheetList-json字符串-------:" + sheetTypes);
                    //处理json


                    JArray arrySheetTypes = JArray.Parse(sheetTypes);
                    for (int i = 0; i < arrySheetTypes.Count; i++)
                    {
                        JObject objType = JObject.Parse(arrySheetTypes[i].ToString());
                        string typeName = objType["TypeName"].ToString();
                        // HF.Cloud.BLL.Common.Logger.Error("------typeName:-----"+ typeName);
                        JArray arrySheets = JArray.Parse(objType["Detail"].ToString());
                        if (arrySheets.Count > 0)
                        {
                            //htmlstr.Append("<div>" + typeName + "</div>");
                            htmlstr.Append("<div class=\"sheetType-listTitle\">" + typeName + "</div>");
                            for (int j = 0; j < arrySheets.Count; j++)
                            {
                                JObject objSheet = JObject.Parse(arrySheets[j].ToString());
                                string sheetID = objSheet["ID"].ToString();
                                string clientName = objSheet["ClientName"].ToString();
                                string sheetMessage = objSheet["Message"].ToString();
                                string writeTime = objSheet["WriteTime"].ToString();
                                //htmlstr.Append("<div>ID:" + sheetID + "客户：" + clientName + "</div>");
                                //htmlstr.Append("<div><a href='SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID=" + sheetID + "'>状态:" + objSheet["SheetStateCN"].ToString() + "消息：" + objSheet["WriteTime"].ToString() + "</a>---" + objSheet["WriteTime"].ToString() + "</div>");

                                htmlstr.Append("<div class=\"sheetType-div\">");
                                htmlstr.Append("<a href=\"SheetDetail.aspx?dd_nav_bgcolor=FF5E97F6&sheetID=" + sheetID + "\">");
                                htmlstr.Append("<div style=\"height:30px;\">");
                                htmlstr.Append("<label class=\"sheetType-sheetID\">" + sheetID + "</label>");
                                htmlstr.Append("<label class=\"sheetType-customName\">" + clientName + "</label>");
                                htmlstr.Append("</div>");
                                htmlstr.Append("<p class=\"sheetType-sheetContent\">"+ sheetMessage + "</p>");
                                htmlstr.Append("<p class=\"sheetType-time\">"+ writeTime + "</p>");
                                htmlstr.Append("</a></div >");
                                htmlstr.Append("");
                                //HF.Cloud.BLL.Common.Logger.Error("---------表单信息-----ID：" + sheetID + "clientName:" + clientName);



                            }
                        }
                    }

                }

            }




        }
    }
}