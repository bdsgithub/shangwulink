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
    public partial class PatrolType : Base.BasePage
    {
        public StringBuilder htmlstr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.InspectBL inspectBLL = new InspectBL();
                DataTable dt = new DataTable();

                try
                {
                    //planType 说明计划是未执行（0）、已完成（1）、正在执行（2）
                    dt = inspectBLL.GetInspectPlanList("2", MainID.ToString(), UserID.ToString(), "0");
                }
                catch (Exception err)
                {
                    HF.Cloud.BLL.Common.Logger.Error("PatrolType.aspx GetInspectPlanList Error", err);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                   for(int i=0;i<dt.Rows.Count;i++)
                    {
                        htmlstr.Append("<div class=\"sheetType-div\">");
                        htmlstr.Append("<a href=\"PatrolList.aspx?dd_nav_bgcolor=FF5E97F6&patrolID=" + dt.Rows[i]["ID"] + "&clientName=" + dt.Rows[i]["ClientName"] + "&executeDate=" + dt.Rows[i]["ExecuteDate"] + "\">");
                        htmlstr.Append("<div style=\"height:30px;\">");
                        //htmlstr.Append("<label class=\"sheetType-sheetID\"></label>");//样式原因需要保留
                        htmlstr.Append("<label class=\"sheetType-customName\">" + dt.Rows[i]["ClientName"] + "</label>");
                        htmlstr.Append("</div>");
                        htmlstr.Append("<p class=\"sheetType-sheetContent\">执行时间：" + dt.Rows[i]["ExecuteDate"] + "</p>");
                        htmlstr.Append("<p class=\"sheetType-time\">" + dt.Rows[i]["ExecuteUserName"] + "</p>");
                        htmlstr.Append("</a></div >");
                    }
                }




              



            }
        }
    }
}