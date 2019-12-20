using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.CustomerWeb.Common
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltlError.Text = Context.Server.UrlDecode(Request.QueryString["er"].Trim());
        }
    }
}