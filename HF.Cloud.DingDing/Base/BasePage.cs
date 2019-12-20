using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HF.Cloud.DingDing.auth;

namespace HF.Cloud.DingDing.Base
{
    public class BasePage:Page
    {
        /// <summary>
        /// 用户Id 主键
        /// </summary>
        protected long UserID = -1;
        /// <summary>
        /// 姓名
        /// </summary>
        protected string UserName = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        protected string UserCode = string.Empty;
        /// <summary>
        /// 用户手机号      通过钉钉获取的手机号来查找用户
        /// </summary>
        protected string UserTel = string.Empty;
        /// <summary>
        /// 服务商ID  主键
        /// </summary>
        protected long MainID = -1;


        /// <summary>
        /// 钉钉里的userid
        /// </summary>
        protected string dd_Userid = string.Empty;

    






        protected override void OnInit(EventArgs e)
        {
            //HttpCookie cookie = new HttpCookie("Mobile");
            //cookie.Value = "18703681842";
            //HttpContext.Current.Response.Cookies.Add(cookie);
            //通过cookie获取从钉钉拿过来的手机号
            BLL.Common.Logger.Error("OnInit进来了！");
            //BLL.Common.Logger.Error("BasePage里获取cookie的Mobile01:" + System.Web.HttpContext.Current.Request.Cookies["Mobile"]);
            HttpCookie cookieMobile = System.Web.HttpContext.Current.Request.Cookies["Mobile"];
            HttpCookie cookieUserid = System.Web.HttpContext.Current.Request.Cookies["Userid"];
            //BLL.Common.Logger.Error("BasePage里获取cookie的Mobile:" + cookieMobile);
          
            if (cookieMobile != null && !string.IsNullOrEmpty(cookieMobile.Value)&& cookieUserid != null && !string.IsNullOrEmpty(cookieUserid.Value))
            {
                dd_Userid = cookieUserid.Value;//钉钉里的userid

                UserTel = cookieMobile.Value;//手机号
                //通过手机号查询用户表
                Model.SB_UserEL user = new Model.SB_UserEL();
                user.UserTel = cookieMobile.Value;
                user.ExecuteEL(46);//Select SB_User By UserTel
                if (user != null && user.ID > 0)
                {
                    BLL.Common.Logger.Error("人员信息user.UserName:" + user.UserName + "人员信息user.ID:" + user.ID + "人员信息user.UserTel:" + user.UserTel);

                    UserID = user.ID;
                    UserName = user.UserName;
                    UserCode = user.UserCode;
                    MainID = user.MainID;
                    base.OnInit(e);
                }
                else
                {
                    //此用户还没有易维客账号
                    BLL.Common.Logger.Error("通过手机号---" + UserTel + "---没有获取到数据库中人员信息");
                    Response.Redirect("/DingDing/ErrorPage.aspx");
                }
            }
            else
            {
                //此用户还没有易维客账号
                //跳转到index页面去获取cookie
                BLL.Common.Logger.Error("ddConfig.js文件，没有获取到cookie，跳转回Index.aspx页面");
              Response.Redirect("../Index.aspx");
            }




        }





    }
}