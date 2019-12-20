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
    public partial class QBCodePrint : BasePage
    {
        protected StringBuilder html = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mainIdS = GetQueryString("mainId");
                long mainId = 0;

                if (mainIdS != string.Empty)
                {
                    mainId = long.Parse(mainIdS);
                }

                this.hidMainID.Value = mainId.ToString();
                GenerateHtml();
            }
        }

        #region QueryString 

        /// <summary>
        /// 打印类型
        /// </summary>
        /// <returns></returns>
        protected string GetPrintType()
        {
            string pt = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["pt"]))
            {
                pt = Request.QueryString["pt"].Trim();
            }

            return pt;
        }

        /// <summary>
        /// 打印数量
        /// </summary>
        /// <returns></returns>
        protected int GetPrintNumber()
        {
            int pt = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["pn"]))
            {
                pt = int.Parse(Request.QueryString["pn"].Trim());
            }

            return pt;
        }

        /// <summary>
        /// 开始打印Id
        /// </summary>
        /// <returns></returns>
        protected long GetPrintStartId()
        {
            long pt = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            {
                pt = long.Parse(Request.QueryString["sid"].Trim());
            }

            return pt;
        }

        /// <summary>
        /// 结束打印Id
        /// </summary>
        /// <returns></returns>
        protected long GetPrintEndId()
        {
            long pt = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["eid"]))
            {
                pt = long.Parse(Request.QueryString["eid"].Trim());
            }

            return pt;
        }

        /// <summary>
        /// 获得模板Id
        /// </summary>
        /// <returns></returns>
        protected long GetTemplateId()
        {
            long pt = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                pt = long.Parse(Request.QueryString["tid"].Trim());
            }

            return pt;
        }


        #endregion

        /// <summary>
        /// 生成Html
        /// </summary>
        protected void GenerateHtml()
        {
            string sqlWhere = string.Empty;

            string pt = GetPrintType();
            int pn = GetPrintNumber();
            long sid = GetPrintStartId();
            long eid = GetPrintEndId();

            long tid = GetTemplateId();

            string mainIdS = GetQueryString("mainId");
            long mainId = 0;

            if (mainIdS != string.Empty)
            {
                mainId = long.Parse(mainIdS);
            }

            if (!string.IsNullOrEmpty(pt))
            {
                DataTable dt = new DataTable();
                DataTable detailTable = new DataTable();

                Model.QB_CodeEL qbCode = new Model.QB_CodeEL();

                Model.QB_ModelDetailEL modelDetail = new Model.QB_ModelDetailEL();

                if (pt == "newprint")
                {
                    qbCode.MainID = mainId;
                    qbCode.ID = sid;
                    qbCode.AssetID = eid;//结束Id

                    dt = qbCode.ExecDT(54);
                }
                else if (pt == "oldprint")
                {
                    dt = qbCode.ExecuteSqlString(GetQBCodeSql(pn));
                }

                modelDetail.MainID = mainId;
                modelDetail.QBID = tid;
                detailTable = modelDetail.ExecDT(52);

                Model.QB_ModelEL model = new Model.QB_ModelEL();
                model.ID = tid;
                model.ExecuteEL(4);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //bool isNewPage = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        #region 2017-8-30 老版本原代码01
                        //if (isNewPage)
                        //{
                        //    html.Append("<div style='height:48px;'></div><div class='bar'>");
                        //}

                        //html.Append("<div class='bar-code' style='padding:" + (model.Padding == string.Empty ? "0px 25px 0px 20px" : model.Padding) + ";height:" + model.QBHeight.ToString() + "px'>");
                        //html.Append("<div class='barc-left'>");
                        //html.Append("<img src='" + dt.Rows[i]["QBPath"] + "' width='" + model.ImgWidth.ToString() + "px' height='" + model.ImgHeight.ToString() + "px' alt=''/>");
                        //html.Append("<div class='barc-left-code' pkey='code' pvalue='" + dt.Rows[i]["QBCode"] + "' >编号：" + dt.Rows[i]["QBCode"] + "</div>");
                        //html.Append("</div>");
                        //html.Append("<div class='barc-right'>");
                        //if (detailTable.Rows.Count > 0)
                        //{
                        //    html.Append("<h4>" + detailTable.Rows[0]["QBDetail"] + "</h4>");
                        //    for (int j = 0; j < detailTable.Rows.Count; j++)
                        //    {
                        //        if (j == 0)
                        //        {
                        //            continue;
                        //        }
                        //        html.Append("<p>" + detailTable.Rows[j]["QBDetail"] + "</p>");
                        //    }
                        //}
                        //else
                        //{
                        //    html.Append("<p></p>");
                        //}
                        //html.Append("</div>");
                        #endregion


                        #region 新代码
                        //html.Append("<div class='tag' style='padding:" + (model.Padding == string.Empty ? "40px 25px 0px 20px;" : model.Padding) + ";width:" + model.QBWidth + "px;height:" + model.QBHeight + "px;float:left;'>");
                        //html.Append("<img src='" + dt.Rows[i]["QBPath"] + "' width='" + model.ImgWidth.ToString() + "px' height='" + model.ImgHeight.ToString() + "px' alt=''/>");
                        //html.Append("<div class='tag-right' style=\"float:right;width:170px;margin-top:10px;margin-right:10px;\">");
                        //if (detailTable.Rows.Count > 0)
                        //{
                        //    html.Append("<h4>" + detailTable.Rows[0]["QBDetail"] + "</h4>");

                        //    for (int j = 0; j < detailTable.Rows.Count; j++)
                        //    {
                        //        if (j == 0)
                        //        {
                        //            continue;
                        //        }
                        //        html.Append("<p>" + detailTable.Rows[j]["QBDetail"] + "</p>");
                        //    }
                        //}
                        //else
                        //{
                        //    html.Append("<p></p>");
                        //}
                        //html.Append("</div>");

                        //html.Append("<div style='text-align:center;width:" + model.ImgWidth.ToString() + "px;' pkey='code' pvalue='" + dt.Rows[i]["QBCode"] + "' >编号：" + dt.Rows[i]["QBCode"] + "</div>");
                        //html.Append("</div>");
                        //html.Append("</div>");//tag
                        #endregion


                        #region 2017-8-30老版本原代码02
                        //html.Append("</div>");//bar

                        //if ((i + 1) % (model.PrintW != 0 ? (model.PrintW * model.PrintH) : 21) != 0)
                        //{
                        //    isNewPage = false;
                        //}
                        //else
                        //{
                        //    isNewPage = true;

                        //    html.Append("</div>");
                        //}

                        //if (i == dt.Rows.Count - 1 && (i + 1) % (model.PrintW != 0 ? (model.PrintW * model.PrintH) : 21) != 0)
                        //{
                        //    html.Append("</div>");
                        //}
                        #endregion

                        #region   版本二
                        ////版本二 2017-9-5
                        //html.Append("<li>");
                        //html.Append("<div class='outer'>");
                        //if (detailTable.Rows.Count > 0)
                        //{
                        //    html.Append("<span class='tel'>" + detailTable.Rows[0]["QBDetail"] + "</span>");
                        //}
                        //html.Append("<div class='inner'>");
                        //html.Append("<img class='QR' src='" + dt.Rows[i]["QBPath"] + "'>");
                        //html.Append("<span class='num'  pkey='code' pvalue='" + dt.Rows[i]["QBCode"] + "'>编号：" + dt.Rows[i]["QBCode"] + "</span>");
                        //html.Append("</div>");
                        //html.Append("</div>");
                        //html.Append("<div class='PageNext'></div>");
                        //html.Append("</li>");
                        #endregion


                        html.Append("<li>");
                        html.Append("<div class='outer'>");
                        if (detailTable.Rows.Count > 0)
                        {
                            html.Append("<span class='title'>" + detailTable.Rows[0]["QBDetail"] + "</span>");
                            html.Append("<span class='tel'>" + detailTable.Rows[1]["QBDetail"] + "</span>");
                        }


                        html.Append("<div class='inner'>");
                        html.Append("<img class='QR' src='" + dt.Rows[i]["QBPath"] + "'>");
                        html.Append("<div class='num'  pkey='code' pvalue='" + dt.Rows[i]["QBCode"] + "'>编号：" + dt.Rows[i]["QBCode"] + "</div>");
                        html.Append("</div>");
                        html.Append("</div>");
                        html.Append("<div class='PageNext'></div>");
                        html.Append("</li>");
                        html.Append("");








                    }
                }

            }
        }

        /// <summary>
        /// 根据传入的打印数量，加载相应数量的标签
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetQBCodeSql(int count)
        {
            //获取当前模板前缀
            Model.QB_ModelEL model = new Model.QB_ModelEL { ID = GetTemplateId() };
            DataTable dt_Model = model.ExecDT(4);
            string strpre = "";
            if (dt_Model.Rows.Count > 0)
            {
                strpre = dt_Model.Rows[0]["ENTag"].ToString();
            }


            string mainIdS = GetQueryString("mainId");
            long mainId = 0;

            if (mainIdS != string.Empty)
            {
                mainId = long.Parse(mainIdS);
            }

            string sql = "select top " + count + " * from QB_Code Where [Valid]='1' and MainID=" + mainId + " and PrintNum=0 and  QBCode like '" + strpre + "%'";

            return sql;
        }
    }
}