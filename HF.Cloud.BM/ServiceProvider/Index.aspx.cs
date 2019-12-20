using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.Model;
using System.Data;
using System.Transactions;

namespace HF.Cloud.BM.ServiceProvider
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataList();
            }
        }

        protected void LnkBtnDelete_Command(object sender, CommandEventArgs e)
        {
            string cn = e.CommandName;
            string cnArgs = e.CommandArgument.ToString();
            SB_UserMEL userM = new SB_UserMEL();
            int flag;

            using (TransactionScope scope = new TransactionScope())
            {
                switch (cn)
                {
                    case "Stop":
                        userM.ID = long.Parse(cnArgs);
                        userM.ExecNonQuery(3, out flag);

                        SB_UserEL user = new SB_UserEL()
                        {
                            Valid = 0,
                            MainID = userM.ID
                        };

                        user.ExecNonQuery(25, out flag);

                        BindDataList();
                        break;
                    case "Enable":
                        userM.ID = long.Parse(cnArgs);
                        userM.ExecNonQuery(31, out flag);

                        SB_UserEL userE = new SB_UserEL()
                        {
                            Valid = 1,
                            MainID = userM.ID
                        };

                        userE.ExecNonQuery(25, out flag);

                        BindDataList();

                        break;
                }

                scope.Complete();
            }
        }

        protected void BindDataList()
        {
            SB_UserMEL userm = new SB_UserMEL();

            DataSet ds = userm.GetPageList(this.AspNetPager1.CurrentPageIndex, string.Empty, OrderType.desc);

            if (ds != null)
            {
                this.AspNetPager1.RecordCount = PagerInfo.GetRecordCount(ds);

                this.rptDataList.DataSource = ds;
                this.rptDataList.DataBind();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindDataList();
        }

        protected void rptDataList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;

                if (drv != null)
                {
                    if (drv["Valid"].ToString() == "0")
                    {
                        LinkButton BtnStopUse = e.Item.FindControl("LnkBtnStopUse") as LinkButton;

                        if (BtnStopUse != null)
                        {
                            BtnStopUse.Text = "启用";
                            BtnStopUse.CommandName = "Enable";
                        }
                    }
                }
            }
        }
    }
}