using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.Model;
using System.Data;
using System.Web.Script.Serialization;

namespace HF.Cloud.BLL
{
  public  class WX_TokenBLL
    {
        /// <summary>
        /// 获取小程序的token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            string token = string.Empty;
            //1,查看数据库中是否有未过期的token，有的话就用此token
            WX_TokenEL wx_TokenEL = new WX_TokenEL();
            DataTable dt = wx_TokenEL.ExecDT(3);
            HF.Cloud.BLL.Common.Logger.Error("GetToken方法查询表结果：" + dt.Rows.Count);
            if (dt.Rows.Count>0)
            {
                string tokenStr=dt.Rows[0]["Token"].ToString();
                HF.Cloud.BLL.Common.Logger.Error("GetToken方法,表中获取的token：" + tokenStr);
                Dictionary<string, object> json_Token = new Dictionary<string, object>();
                JavaScriptSerializer js_Token = new JavaScriptSerializer();
                js_Token.MaxJsonLength = int.MaxValue;
                json_Token = js_Token.Deserialize<Dictionary<string, object>>(tokenStr);
                if(json_Token.Keys.Contains<string>("access_token"))
                {
                    token = json_Token["access_token"].ToString();
                }
            }
            //2,数据库中没有未过期的token，则从新请求小程序服务器获取最新token
            else
            {
                string appId = System.Configuration.ConfigurationManager.AppSettings["AppId"];
                string appSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];

                string url_Str = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential"+ 
                    "&appid=" + appId+ 
                    "&secret=" + appSecret;
               
                WeChatAppDecrypt wcad = new WeChatAppDecrypt();
                string tokenStr = wcad.GetResponse(url_Str);
                HF.Cloud.BLL.Common.Logger.Error("GetToken方法,接口中获取的token：" + tokenStr);
                token = GetValueByKey(tokenStr, "access_token");
                //2.1，把当前的token表记录设置为无效
                int ra;
                long returnValue_Up=wx_TokenEL.ExecNonQuery(2, out ra);
                //2.2，把最新的token添加到数据库中
                wx_TokenEL.Token = tokenStr;
                wx_TokenEL.CreateTime = DateTime.Now.ToString();
                wx_TokenEL.Valid = 1;
                long returnValue_Add=wx_TokenEL.ExecNonQuery(1, out ra);
            }
            HF.Cloud.BLL.Common.Logger.Error("GetToken方法获取token值：" + token);
            return token;
        }


        /// <summary>
        /// 获取json里的值
        /// </summary>
        /// <param name="tokenStr">接口获取的json字符串</param>
        /// <param name="key">要获取值的key</param>
        /// <returns></returns>
        public string GetValueByKey(string tokenStr,string key)
        {
            string value = "";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = int.MaxValue;
            if (!string.IsNullOrEmpty(tokenStr) && !string.IsNullOrEmpty(key))
            {
                dic = json.Deserialize<Dictionary<string, object>>(tokenStr);
                if (dic.Keys.Contains(key))
                {
                    value = dic[key].ToString();
                }
            }
            return value;
        }





    }
}
