using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using HF.Cloud.BLL;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;//引用Newtonsoft.Json

namespace HF.Cloud.DingDing.Repair
{
    public partial class RepairList : Base.BasePage
    {
        public StringBuilder htmlstr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RepairType"]))
                {
                    string repairType = Request.QueryString["RepairType"].ToString();

                    BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
                    DataTable dt = new DataTable();
                    List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();

                    try
                    {
                        if (repairType == "All")  //所有维修单
                        {
                            json = repairBLL.GetTaskList(MainID);
                            if(json.Count>0)
                            {
                                for(int j=0;j<json.Count;j++)
                                {
                                    htmlstr.Append("<div class=\"sheetType-listTitle\">" + json[j]["AcceptName"] + "</div>");
                                    HF.Cloud.BLL.Common.Logger.Error("------AcceptName:-----" + json[j]["AcceptName"]);
                                    List<Dictionary<string, object>> json_List = (List<Dictionary<string, object>>)json[j]["NowAcceptTask"];
                                    for(int li=0;li<json_List.Count;li++)
                                    {
                                        string repairID = json_List[li]["ID"].ToString();
                                        string clientName = json_List[li]["ClientName"].ToString(); 
                                        string typeName = json_List[li]["TypeName"].ToString();
                                        string writeTime = json_List[li]["WriteTime"].ToString();
                                        HF.Cloud.BLL.Common.Logger.Error("------所有维修工单-----工单号:" + repairID + "客户名称："+ clientName);
                                        htmlstr.Append("<div class=\"sheetType-div\">");
                                        htmlstr.Append("<a href=\"RepairDetail.aspx?dd_nav_bgcolor=FF5E97F6&repairID=" + repairID + "\">");
                                        htmlstr.Append("<div  style=\"height:30px;\">");
                                        htmlstr.Append("<label class=\"sheetType-sheetID\">" + repairID + "</label>");
                                        htmlstr.Append("<label class=\"sheetType-customName\">" + clientName + "</label>");
                                        htmlstr.Append("</div>");
                                        htmlstr.Append("<p class=\"sheetType-sheetContent\">" + typeName + "</p>");
                                        htmlstr.Append("<p class=\"sheetType-time\">" + writeTime + "</p>");
                                        htmlstr.Append("</a></div >");

                                    }
                                }
                            }
                        }
                        else
                        {
                            dt = repairBLL.GetTaskList(repairType, MainID, UserID);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for(int i=0;i<dt.Rows.Count;i++)
                                {
                                    string repairID = dt.Rows[i]["ID"].ToString();  //维修工单id
                                    string clientName = dt.Rows[i]["ClientName"].ToString();
                                    string typeName = dt.Rows[i]["TypeName"].ToString();
                                    string writeTime = dt.Rows[i]["WriteTime"].ToString();
                                    HF.Cloud.BLL.Common.Logger.Error("------维修工单-----工单号:" + repairID + "客户名称：" + clientName);
                                    htmlstr.Append("<div class=\"sheetType-div\">");
                                    htmlstr.Append("<a href=\"RepairDetail.aspx?dd_nav_bgcolor=FF5E97F6&repairID=" + repairID + "\">");
                                    htmlstr.Append("<div style=\"height:30px;\">");
                                    htmlstr.Append("<label class=\"sheetType-sheetID\">" + repairID + "</label>");
                                    htmlstr.Append("<label class=\"sheetType-customName\">" + clientName + "</label>");
                                    htmlstr.Append("</div>");
                                    htmlstr.Append("<p class=\"sheetType-sheetContent\">" + typeName + "</p>");
                                    htmlstr.Append("<p class=\"sheetType-time\">" + writeTime + "</p>");
                                    htmlstr.Append("</a></div >");
                                    htmlstr.Append("");
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        BLL.Common.Logger.Error("RepairList GetTaskList Error", err);
                    }
                }
            }

        }
    }
}