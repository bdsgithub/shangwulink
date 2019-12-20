using HF.Cloud.BLL;
using HF.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.BM.Labels
{
    public partial class QBModelList : BasePage
    {
        protected StringBuilder ModelHtml = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindServiceProvider();

                string mid = GetQueryString("mainId");

                if (!string.IsNullOrEmpty(mid))
                {
                    this.ddlSPList.SelectedValue = mid;
                }
                InitPage();

            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            long mainId = 0;

            if (this.ddlSPList.SelectedIndex > 0)
            {
                mainId = long.Parse(this.ddlSPList.SelectedValue);
            }


            if (mainId > 0)
            {
                DataTable dtModel = new DataTable();
                QB_ModelEL elModel = new QB_ModelEL();
                elModel.MainID = mainId;
                elModel.Valid = 1;
                dtModel = elModel.ExecDT(51);

                DataTable dtDetail = new DataTable();
                QB_ModelDetailEL elDetail = new QB_ModelDetailEL();
                elDetail.MainID = mainId;
                dtDetail = elDetail.ExecDT(51);


                foreach (DataRow drModel in dtModel.Rows)
                {
                    ModelHtml.Append("<div class=\"label-manage\" >");
                    ModelHtml.Append("<p>" + drModel["ModelName"] + "</p>");
                    ModelHtml.Append("<br/>");
                    ModelHtml.Append("<div class=\"bo label-facade\">");
                    ModelHtml.Append("<div class=\"label-con-l\"  style=\"margin-left:50px;\">");
                    //2017-9-6
                    DataRow[] drs = dtDetail.Select("QBID=" + drModel["ID"]);
                    for (int j = 0; j < drs.Length; j++)
                    {
                        ModelHtml.Append("<p>" + drs[j]["QBDetail"] + "</p>");
                    }

                    ModelHtml.Append("<img src=\"/image/QRCode.png\" alt =\"二维码\" class=\"bo\" />");
                    ModelHtml.Append("<p>编号：" + drModel["ENTag"] + 10000000000.ToString().Substring(drModel["ENTag"].ToString().Trim().Length + 2) + "1" + "</p>");
                    ModelHtml.Append("</div>");
                    //ModelHtml.Append("<div class=\"label-con-r\">");
                    //DataRow[] drs = dtDetail.Select("QBID=" + drModel["ID"]);
                    //for (int j = 0; j < drs.Length; j++)
                    //{
                    //    ModelHtml.Append("<p>" + drs[j]["QBDetail"] + "</p>");
                    //}
                    //ModelHtml.Append("</div>");
                    ModelHtml.Append("</div>");
                    ModelHtml.Append("<div class=\"label-compile\">");
                    //ModelHtml.Append("<p><span>尺寸：63.5mm*" + (drModel["PrintH"].ToString() == "6" ? "46.6" : "38.1") + "mm</span></p>");
                    //ModelHtml.Append("<p><span>二维码：26mm</span></p>");
                    //ModelHtml.Append("<p><span>A4排版：" + drModel["PrintW"] + "*" + drModel["PrintH"] + "</span></p>");
                    ModelHtml.Append("<div class=\"label-button\">");
                    ModelHtml.Append("<a href=\"QBModePrintSlt.aspx?tid=" + drModel["ID"] + "&mainId=" + this.ddlSPList.SelectedValue + "\" ><img src=\"../image/label-p.png\" alt=\"\" /><span> 打印</span></a>");
                    ModelHtml.Append("</div>");
                    ModelHtml.Append("<div class=\"label-button\">");
                    ModelHtml.Append("<a href=\"QBModelAdd.aspx?id=" + drModel["ID"] + "&mainId=" + this.ddlSPList.SelectedValue + "\" ><img src=\"../image/label-c.png\" alt=\"\" /><span> 编辑</span></a>");
                    ModelHtml.Append("</div>");
                    ModelHtml.Append("<div class=\"label-button\">");
                    ModelHtml.Append("<a href=\"javascript:$.DeleteModel(" + drModel["ID"] + ")\"><img src=\"../image/label-r.png\" alt=\"\" /><span> 删除</span></a>");
                    ModelHtml.Append("</div>");
                    ModelHtml.Append("</div>");
                    ModelHtml.Append("</div>");
                }
            }
        }

        /// <summary>
        /// 绑定服务商
        /// </summary>
        private void BindServiceProvider()
        {
            UserMBLL userMBLL = new UserMBLL();

            userMBLL.BindDropDownListService(this.ddlSPList, 5);
        }

        protected void ddlSPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitPage();
        }
    }
}