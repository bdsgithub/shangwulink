using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.BLL;
using HF.Cloud.DingDing.auth;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace HF.Cloud.DingDing
{
    public partial class Index : System.Web.UI.Page
    {
        public string configStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            HF.Cloud.BLL.Common.Logger.Error("index.aspx页面的logger进来了");
            configStr = AuthHelper.getConfig(Request.Url.ToString());//url传入当前的url
            HF.Cloud.BLL.Common.Logger.Error("configStr:" + configStr);


            



        }
    }
}