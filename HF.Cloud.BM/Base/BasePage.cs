using System;
using System.Data;
using System.Web;
using System.Web.UI;

namespace HF.Cloud.BM
{
    public class BasePage : Page
    {
        /// <summary>
        /// 登陆ID
        /// </summary>
        protected long UserId = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        protected string UserCode = string.Empty;

        /// <summary>
        /// 用户名称
        /// </summary>
        protected string UserName = string.Empty;

        /// <summary>
        /// 用户唯一码
        /// </summary>
        protected string CustomerUniqueCode = string.Empty;


        /// <summary>
        /// 登陆用户所属客户Id
        /// </summary>
        protected long CustomerId = 0;

        protected long CustomerIDReal = 0;
        /// <summary>
        /// 登陆用户所属客户名称
        /// </summary>
        protected string CustomerName = string.Empty;

        /// <summary>
        /// 登陆用户所属客户编码
        /// </summary>
        protected string CustomerCode = string.Empty;

        /// <summary>
        /// 服务商Id
        /// </summary>
        protected long ServiceId = 0;

        /// <summary>
        /// 服务商名称
        /// </summary>
        protected string ServiceName = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "layer", "<script type='text/javascript' src='/js/layer/layer.js'></script>");
            HttpCookie cookieUserId = Request.Cookies["UserID"];
            HttpCookie cookieUserCode = Request.Cookies["UserCode"];
            if (cookieUserId != null && !string.IsNullOrEmpty(cookieUserId.Value)
                && cookieUserCode != null && !string.IsNullOrEmpty(cookieUserCode.Value) &&
                cookieUserId.Value != "-1")
            {
                UserId = long.Parse(cookieUserId.Value);
                UserCode = cookieUserCode.Value;

                Model.C_B_CustomerEL customer = new Model.C_B_CustomerEL()
                {
                    ID = UserId
                };

                customer.ExecuteEL(4);

                if (customer != null)
                {
                    this.UserName = customer.CCustomerName;
                    this.CustomerId = customer.CMID;
                    this.CustomerIDReal = customer.ID;
                    this.CustomerUniqueCode = customer.CUniqueCode;

                    Model.C_ClientEL client = new Model.C_ClientEL()
                    {
                        ID = this.CustomerId
                    };

                    client.ExecuteEL(4);

                    if (client != null)
                    {
                        this.CustomerName = client.ClientName;
                        this.CustomerCode = client.ClientCode;
                        if (Session["mainInfo"] != null)
                        {
                            this.ServiceId = long.Parse(Session["mainInfo"].ToString());//获取服务商
                        }
                        else
                        {
                            this.ServiceId = client.MainID;
                        }
                        this.ServiceName = BLL.UserBLL.GetServiceProviderNameById(this.ServiceId);
                        //this.ServiceId = client.MainID;

                        //this.ServiceName = BLL.UserBLL.GetServiceProviderNameById(this.ServiceId);


                    }
                    else
                    {
                        //Response.Redirect("/login.aspx");
                    }
                }
                else
                {
                    //Response.Redirect("/login.aspx");
                }
            }
            else
            {
                //Response.Redirect("/login.aspx");
            }

            base.OnInit(e);
        }

        /// <summary>
        /// 获取QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetQueryString(string key)
        {
            string gqs = string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString[key]))
            {
                gqs = Request.QueryString[key];
            }

            return gqs;
        }
    }
}