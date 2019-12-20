using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.Sheet
{
    public partial class SheetType:Base.BasePage
    {
        public StringBuilder htmlStr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            SheetBL shBLL = new SheetBL();
            DataTable dt = new DataTable();
            try
            {
                dt = shBLL.GetAPPMainShow(MainID, UserID);//获取人员所在组
                HF.Cloud.BLL.Common.Logger.Error("SheetType.aspx页面dt.rows:" + dt.Rows.Count + "---MainID:" + MainID + "----userID：" + UserID);
                //dt.Columns.Add("NoAccepted", typeof(int));//未受理
                //dt.Columns.Add("MyNoFinish", typeof(int));//我的未完成
                //dt.Columns.Add("TeamNoFinish", typeof(int));//组未完成
                //dt.Columns.Add("AllNoFinish", typeof(int));//所有未完成
                //dt.Columns.Add("MyFinish", typeof(int)); //我已完成
                //dt.Columns.Add("All", typeof(int));//全部工单
                //dt.Columns.Add("Urgent", typeof(int)); //紧急工单
            }
            catch (Exception err)
            {
                HF.Cloud.BLL.Common.Logger.Error("GetSheetListByStatus Error", err);
            }

            //for(int i=0;i<7;i++)
            //{
            //    htmlStr+= "<div> <label>"+ dt.Columns[i].ToString() + "</label> <label>"+ dt.Rows[0][i].ToString() + "</label></div>";
            //}
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[0].ToString() + "\" >未受理 </ a ><label>" + dt.Rows[0][0].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[1].ToString() + "\" >我的未完成 </ a ><label>" + dt.Rows[0][1].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[2].ToString() + "\" > 组未完成</ a ><label>" + dt.Rows[0][2].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[3].ToString() + "\" > 所有未完成</ a ><label>" + dt.Rows[0][3].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[4].ToString() + "\" >我已完成 </ a ><label>" + dt.Rows[0][4].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[5].ToString() + "\" >全部工单 </ a ><label>" + dt.Rows[0][5].ToString() + "</label></div>");
            //htmlStr.Append("<div><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[6].ToString() + "\" > 紧急工单</ a ><label>" + dt.Rows[0][6].ToString() + "</label></div>");

            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[0].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >未受理 </label><label class=\"label_style\">" + dt.Rows[0][0].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[1].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >我的未完成 </label><label class=\"label_style\">" + dt.Rows[0][1].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[2].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >组未完成 </label><label class=\"label_style\">" + dt.Rows[0][2].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[3].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >所有未完成</label><label class=\"label_style\">" + dt.Rows[0][3].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[4].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >我已完成 </label><label class=\"label_style\">" + dt.Rows[0][4].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[5].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >全部工单 </label><label class=\"label_style\">" + dt.Rows[0][5].ToString() + "</label></a></div></div>");
            htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"SheetList.aspx?dd_nav_bgcolor=FF5E97F6&SheetType=" + dt.Columns[6].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >紧急工单 </label><label class=\"label_style\">" + dt.Rows[0][6].ToString() + "</label></a></div></div>");

        }
    }
}