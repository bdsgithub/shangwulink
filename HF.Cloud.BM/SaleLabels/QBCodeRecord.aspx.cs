using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.Model;
using System.Data;

namespace HF.Cloud.BM.SaleLabels
{
    public partial class QBCodeRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDataList();
            }
        }



        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindDataList();
        }

        protected void BindDataList()
        {
            Q_LabelBatch labelBatch_model = new Q_LabelBatch();
            
            DataSet ds = labelBatch_model.GetDataList(this.AspNetPager1.CurrentPageIndex, "CreateTime", "desc");

            if (ds != null)
            {
                this.AspNetPager1.RecordCount = PagerInfo.GetRecordCount(ds);

                this.rptDataList.DataSource = ds;
                this.rptDataList.DataBind();
            }
        }


    }
}