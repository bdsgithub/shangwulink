using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.Patrol
{
    public partial class PatrolList : Base.BasePage
    {
        public StringBuilder htmlstr = new StringBuilder();
        public string patrolID = string.Empty;// 计划巡检id
        public string clientName = string.Empty;// 客户名称
        public string executeDate = string.Empty;// 执行时间
        public string patrolCount = string.Empty;// 巡检记录数
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { 
                if(!string.IsNullOrEmpty(Request.QueryString["patrolID"].ToString()))
                {
                    patrolID = Request.QueryString["patrolID"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["clientName"].ToString()))
                {
                    clientName = Request.QueryString["clientName"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["executeDate"].ToString()))
                {
                    executeDate = Request.QueryString["executeDate"].ToString();
                }

                BLL.InspectBL inspectBLL = new InspectBL();
                DataTable dt = new DataTable();
                try
                {
                   dt = inspectBLL.GetInspectPlanDetails(long.Parse(patrolID), MainID);
                }
                catch (Exception err)
                {
                    HF.Cloud.BLL.Common.Logger.Error("PatrolList.aspx  Error", err);
                }
                patrolCount = "0";
                if (dt != null && dt.Rows.Count > 0)
                {
                    patrolCount = dt.Rows.Count.ToString();
                    for (int i=0;i<dt.Rows.Count;i++)
                    {
                        htmlstr.Append("<div class=\"sheetType-div\">");
                        htmlstr.Append("<a href=\"PatrolDetail.aspx?dd_nav_bgcolor=FF5E97F6&assetRecordId=" + dt.Rows[i]["AssetRecordId"] +  "\">");
                        htmlstr.Append("<div style=\"height:20px;\">");
                        htmlstr.Append("<p class=\"sheetType-sheetContent\" style=\"font-size:12px;\">" + dt.Rows[i]["aseetExecuDate"] + "</p>");
                        htmlstr.Append("</div>");
                        htmlstr.Append("<p class=\"sheetType-sheetContent\">设备编号：" + dt.Rows[i]["QBCode"] + "</p>");
                        htmlstr.Append("<p class=\"sheetType-sheetContent\" style=\"margin-bottom:15px;\">设备名称：" + dt.Rows[i]["AssetName"] + "</p>");
                        htmlstr.Append("</a></div >");
                    }
                }







            } 
        }
    }
}