using HF.Cloud.BLL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.BM.SaleLabels
{
    public partial class QBModelAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    Model.QB_ModelEL qbmodel = new Model.QB_ModelEL();
                    qbmodel.ID = long.Parse(Request.QueryString["id"].ToString());
                    qbmodel.ExecuteEL(4);

                    #region 给控件赋值
                    if (qbmodel.PrintH == 6)
                    {
                        hidModel.Value = "0";
                    }
                    else
                    {
                        hidModel.Value = "1";
                    }
                    tb_modelName.Text = qbmodel.ModelName;
                    tb_entag.Text = qbmodel.ENTag;

                    ltCode.Text = qbmodel.ENTag + 10000000000.ToString().Substring(qbmodel.ENTag.Trim().Length + 2) + "1";//预览处二维码


                    DataTable dtModelDetail = new Model.QB_ModelDetailEL() { QBID = qbmodel.ID }.ExecDT(41);
                    if (dtModelDetail.Rows.Count > 0)
                    {

                        othertext1.Text = dtModelDetail.Rows[0]["QBDetail"].ToString();
                        lt1.Text = dtModelDetail.Rows[0]["QBDetail"].ToString();
                        try
                        {
                            othertext2.Text = dtModelDetail.Rows[1]["QBDetail"].ToString();
                            lt2.Text = dtModelDetail.Rows[1]["QBDetail"].ToString();
                        }
                        catch
                        {
                            othertext2.Text = "";
                        }
                        //try
                        //{
                        //    othertext3.Text = dtModelDetail.Rows[2]["QBDetail"].ToString();
                        //    lt3.Text = dtModelDetail.Rows[2]["QBDetail"].ToString();
                        //}
                        //catch
                        //{
                        //    othertext3.Text = "";
                        //}
                        //try
                        //{
                        //    othertext4.Text = dtModelDetail.Rows[3]["QBDetail"].ToString();
                        //    lt4.Text = dtModelDetail.Rows[3]["QBDetail"].ToString();
                        //}
                        //catch
                        //{
                        //    othertext4.Text = "";
                        //}
                        //try
                        //{
                        //    othertext5.Text = dtModelDetail.Rows[4]["QBDetail"].ToString();
                        //    lt5.Text = dtModelDetail.Rows[4]["QBDetail"].ToString();
                        //}
                        //catch
                        //{
                        //    othertext5.Text = "";
                        //}
                        //try
                        //{
                        //    othertext6.Text = dtModelDetail.Rows[5]["QBDetail"].ToString();
                        //    lt6.Text = dtModelDetail.Rows[5]["QBDetail"].ToString();
                        //}
                        //catch
                        //{
                        //    othertext6.Text = "";
                        //}
                    }

                    #endregion

                }
            }
        }

        private List<string> getQBModelDetail()
        {
            List<string> lstresult = new List<string>();
            doQBModelDetail(lstresult, othertext1);
            doQBModelDetail(lstresult, othertext2);
            //doQBModelDetail(lstresult, othertext3);
            //doQBModelDetail(lstresult, othertext4);
            //doQBModelDetail(lstresult, othertext5);
            //doQBModelDetail(lstresult, othertext6);

            return lstresult;

        }
        private void doQBModelDetail(List<string> lstres, TextBox tbtext)
        {
            lstres.Add(tbtext.Text);
        }
        private int formatInt(string text)
        {
            Int32 vint;
            Int32.TryParse(text, out vint);
            return vint;
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            #region 赋值实体
            Model.QB_ModelEL qbmodel = new Model.QB_ModelEL();

            qbmodel.MainID = 0;
            qbmodel.Valid = 1;
            qbmodel.ImgWidth = 149;
            qbmodel.ImgHeight = 149;
            qbmodel.QBHeight = 149;
            qbmodel.QBWidth = 149;
            qbmodel.FontSize = 12;
            qbmodel.ENTag = tb_entag.Text;
            qbmodel.ModelName = tb_modelName.Text.Trim();

            if (hidModel.Value == "0")//3*6
            {
                qbmodel.Padding = "18px 25px 18px 20px";
                qbmodel.QBHeight = 216;
                qbmodel.PrintW = 3;
                qbmodel.PrintH = 6;
            }
            else if (hidModel.Value == "1")//3*7
            {
                qbmodel.Padding = "0px 25px 0px 20px";
                qbmodel.QBHeight = 216;
                qbmodel.PrintW = 3;
                qbmodel.PrintH = 7;
            }
            #endregion

            BLL.QBModelBLL bll_qbmodel = new BLL.QBModelBLL();
            if (Request.QueryString["id"] != null)
            {
                qbmodel.ID = long.Parse(Request.QueryString["id"]);
            }
           

            Model.QB_ModelEL qbTmplt = new Model.QB_ModelEL()
            {
                ENTag = tb_entag.Text.Trim()
            };

            if (Request.QueryString["id"] != null)
            {
                qbTmplt.ID = long.Parse(Request.QueryString["id"].Trim());
                qbTmplt.ExecuteEL(53);
            }
            else
            {
                qbTmplt.ExecuteEL(52);
            }

            if (qbTmplt.ID > 0 && qbTmplt.MainID > 0)
            {
                MessageBox.Show(this, "前缀已经使用请重新输入！");
                return;
            }

            if (Request.QueryString["id"] != null)
            {
                #region 编辑  删除此原来模板的内容  添加新的内容

                qbmodel.ExecNonQuery(2);

                List<string> qbmodeldetailstr = getQBModelDetail();
                Model.QB_ModelDetailEL modeldetail = new Model.QB_ModelDetailEL();
                modeldetail.QBID = qbmodel.ID;
                modeldetail.ExecNonQuery(31);//删除此原来模板的内容
                modeldetail.MainID = 0;
                modeldetail.QBID = qbmodel.ID;
                foreach (string strdetail in qbmodeldetailstr)//循环添加新的内容
                {
                    modeldetail.QBDetail = strdetail;
                    modeldetail.ExecNonQuery(1);
                }
                #endregion
            }
            else
            {
                //判断是否存在该名称的模板
                if (bll_qbmodel.ExistsModel(qbmodel.ModelName, qbmodel.ID))
                {
                    BLL.Common.MessageBox.Show(this, "已存在相同名称的模板！");
                    return;
                }
                #region 新增
                qbmodel.ExecuteEL(1);//插入记录  得到新插入实体
                List<string> qbmodeldetailstr = getQBModelDetail();
                Model.QB_ModelDetailEL modeldetail = new Model.QB_ModelDetailEL();
                modeldetail.MainID = 0;
                modeldetail.QBID = qbmodel.ID;
                foreach (string strdetail in qbmodeldetailstr)
                {
                    modeldetail.QBDetail = strdetail;
                    modeldetail.ExecNonQuery(1);
                }
                #endregion
            }

            MessageBox.ShowAndRedirect(this, "保存成功！", "QBModelList.aspx");

        }
    }
}