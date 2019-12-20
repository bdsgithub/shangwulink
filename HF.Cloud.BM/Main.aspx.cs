using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.BM
{
    public partial class Main : BasePage
    {
        protected string logoHtml = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ServiceId == 10055)
            {
                logoHtml = "class='logoIT cient-left-min'";
            }
            else
            {
                logoHtml = "class='logo cient-left-min'";
            }

            if (!IsPostBack)
            {

                this.lblCustomerName.Text = UserName;
                this.lblUserName.Text = UserCode;

                //DataTable dt = new Model.C_CustomerDataSet().GetCustomerMain(CustomerUniqueCode).Tables[0];
                //ddlMain.DataSource = dt;
                //ddlMain.DataTextField = "sbname";
                //ddlMain.DataValueField = "id";
                //ddlMain.DataBind();
                //if (dt.Rows.Count < 1)
                //{
                //    ddlMain.Items.Insert(0, new ListItem(ServiceName, ServiceId.ToString()));
                //}
                //ddlMain.SelectedValue = ServiceId.ToString();
            }
        }


        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMain.SelectedIndex > -1)
            {
                Session["mainInfo"] = ddlMain.SelectedValue;
                Response.Redirect("Main.aspx");
            }
        }
    }
}