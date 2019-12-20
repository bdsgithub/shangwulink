using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HF.Cloud.DingDing.auth;
using System.Web.Script.Serialization;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// userinfo 的摘要说明
    /// </summary>
    public class userinfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //获取Code
            string code = context.Request["code"];

            string accessToken = AuthHelper.getAccessToken();
            /*****通过CODE换取用户身份(用户userId)****/
            // https://oapi.dingtalk.com/user/getuserinfo?access_token=ACCESS_TOKEN&code=CODE
            String url_code = Env.OAPI_HOST + "/user/getuserinfo?" +
                   "access_token=" + accessToken + "&code=" + code;
            //获取用户userId等参数
            string userInfoStr_code = AuthHelper.Request_url(url_code, "GET", "");
            //转为json
            Dictionary<string, object> json_code = new Dictionary<string, object>();
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            json_code = js.Deserialize<Dictionary<string, object>>(userInfoStr_code);

            //获取userid
            string userid = "";
            if (json_code.Keys.Contains("userid"))
            {
                userid = json_code["userid"].ToString();
                HF.Cloud.BLL.Common.Logger.Error("钉钉里的userid：" + userid);
                
               
            }

            /*****通过userId获取用户详情****/
            // https://oapi.dingtalk.com/user/get?access_token=ACCESS_TOKEN&userid=zhangsan
            String url_userid = Env.OAPI_HOST + "/user/get?" +
                 "access_token=" + accessToken + "&userid=" + userid;
            //获取用户name等参数信息
            string userInfoStr_userid = AuthHelper.Request_url(url_userid, "GET", "");
            HF.Cloud.BLL.Common.Logger.Error("钉钉里的user：" + userInfoStr_userid);
            //转为json
            Dictionary<string, object> json_User = new Dictionary<string, object>();
            JavaScriptSerializer js_User = new JavaScriptSerializer();
            js_User.MaxJsonLength = int.MaxValue;
            json_User = js.Deserialize<Dictionary<string, object>>(userInfoStr_userid);


            //把钉钉里的userid添加到数据库中
            string userMobile = json_User["mobile"].ToString(); 
            //通过手机号查询用户表
            Model.SB_UserEL user = new Model.SB_UserEL();
            user.UserTel = userMobile;
            user.ExecuteEL(46);//Select SB_User By UserTel
            HF.Cloud.BLL.Common.Logger.Error("user的DingID：" + user.DingID);
            if (user != null && user.ID > 0)
            {
                if(user.DingID==null)
                {
                    //修改用户的DingID
                    Model.SB_UserEL user_Ding = new Model.SB_UserEL();
                    user_Ding.UserTel = userMobile;
                    user_Ding.DingID = userid;
                    //user.ExecuteEL(24);
                    int rowCount = 0;
                    user_Ding.ExecNonQuery(24, out rowCount);//Update DingID By UserTel and Valid=1 
                    if(rowCount>0)
                    {
                        HF.Cloud.BLL.Common.Logger.Error("手机号：" + userMobile + "修改DingID成功");
                    }
                }
                else
                {
                    HF.Cloud.BLL.Common.Logger.Error("手机号：" + userMobile + "已经有DingID了");
                }
            }

           



            context.Response.Write(userInfoStr_userid);





        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}