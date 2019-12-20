using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HF.Cloud.Model;

namespace HF.Cloud.BM.Common
{
    public partial class TicketList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            Model.Pay.PO_MainTicket mainTicket_model = new Model.Pay.PO_MainTicket();

            DataSet ds = mainTicket_model.GetPageList(this.AspNetPager1.CurrentPageIndex, string.Empty, Model.OrderType.desc);

            if (ds != null)
            {
                this.AspNetPager1.RecordCount = PagerInfo.GetRecordCount(ds);

                this.rptDataList.DataSource = ds;
                this.rptDataList.DataBind();
            }
        }




    }
}