using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.Repair
{
    public partial class RepairType : Base.BasePage
    {
        public StringBuilder htmlStr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
            DataTable dt = new DataTable();
            if (!IsPostBack)
            {
                try
                {
                    dt = repairBLL.GetTypeAndCount(MainID, UserID);
                    HF.Cloud.BLL.Common.Logger.Error("Repair.aspx页面dt.rows:" + dt.Rows.Count + "---MainID:" + MainID + "----userID：" + UserID);
                }
                catch (Exception err)
                {
                    BLL.Common.Logger.Error("RepairType GetTypeAndCount Error", err);
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"RepairList.aspx?dd_nav_bgcolor=FF5E97F6&RepairType=" + dt.Columns[0].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >未分配</label><label class=\"label_style\">" + dt.Rows[0][0].ToString() + "</label></a></div></div>");
                htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"RepairList.aspx?dd_nav_bgcolor=FF5E97F6&RepairType=" + dt.Columns[1].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >我的未完成维修单 </label><label class=\"label_style\">" + dt.Rows[0][1].ToString() + "</label></a></div></div>");
                htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"RepairList.aspx?dd_nav_bgcolor=FF5E97F6&RepairType=" + dt.Columns[2].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >所有维修单 </label><label class=\"label_style\">" + dt.Rows[0][2].ToString() + "</label></a></div></div>");
                htmlStr.Append("<div class=\"oneself-list eweic-top div_height\"><div class=\"innerdiv\"><a href = \"RepairList.aspx?dd_nav_bgcolor=FF5E97F6&RepairType=" + dt.Columns[3].ToString() + "\" ><img class=\"img_style\"  src=\"../img/c.png\" /> <label class=\"label_style\" >我的所有维修单</label><label class=\"label_style\">" + dt.Rows[0][3].ToString() + "</label></a></div></div>");
            }

        }
    }
}