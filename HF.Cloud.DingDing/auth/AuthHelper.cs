using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

using System.Text;

namespace HF.Cloud.DingDing.auth
{
    public class AuthHelper
    {

        //获取accessToken
        public static String getAccessToken()
        {
            string token = string.Empty;//token
            try
            {
                //先看数据库中的accessToken可用不，可用就用数据库中的，不可用就重新生成
                //数据库中获取最新tokenString
                Model.DD_GetToken model = new Model.DD_GetToken();
                model.ExecuteEL(41);
                if (model.ID > 0)
                {
                    string tokenString = model.Token;
                    Dictionary<string, object> json_Token = new Dictionary<string, object>();
                    JavaScriptSerializer js_Token = new JavaScriptSerializer();
                    js_Token.MaxJsonLength = int.MaxValue;
                    json_Token = js_Token.Deserialize<Dictionary<string, object>>(tokenString);

                    if (json_Token.Keys.Contains("access_token"))
                    {
                        token = json_Token["access_token"].ToString();
                    }
                }
                else   //如果数据库中的token和ticket失效则重新从钉钉接口获取
                {
                    //调用钉钉接口获取token
                    String url_Token = Env.OAPI_HOST + "/gettoken?" +
                        "corpid=" + Env.CORP_ID + "&corpsecret=" + Env.SECRET;
                    string tokenString = Request_url(url_Token, "GET", "");  //获取token的字符串

                    Dictionary<string, object> json_Token = new Dictionary<string, object>();
                    JavaScriptSerializer js_Token = new JavaScriptSerializer();
                    js_Token.MaxJsonLength = int.MaxValue;
                    json_Token = js_Token.Deserialize<Dictionary<string, object>>(tokenString);

                    if (json_Token.Keys.Contains("access_token"))
                    {
                        token = json_Token["access_token"].ToString();
                        //调用钉钉接口获取ticket
                        String url_Ticket = Env.OAPI_HOST + "/get_jsapi_ticket?" +
                           "type=jsapi" + "&access_token=" + token;
                        string ticketString = Request_url(url_Ticket, "GET", "");

                        //原数据已经过期了，将获取的最新token和ticket更新数据库
                        model.ExecNonQuery(31);//设置原来数据valid=0失效状态
                        model.Token = tokenString;
                        model.Ticket = ticketString;
                        model.Valid = 1;
                        model.ExecNonQuery(1);//添加数据

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return token;
        }

        #region  单纯获取票据ticket
        //获取票据JsapiTicket
        //public static String getJsapiTicket(String accessToken)
        //{
        //    String url = Env.OAPI_HOST + "/get_jsapi_ticket?" +
        //        "type=jsapi" + "&access_token=" + accessToken;

        //    string ticketString = Request_url(url, "GET", "");

        //    Dictionary<string, object> json = new Dictionary<string, object>();
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    js.MaxJsonLength = int.MaxValue;
        //    json = js.Deserialize<Dictionary<string, object>>(ticketString);


        //    if (json.Keys.Contains("ticket"))
        //    {
        //        return json["ticket"].ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        #endregion

        //获取票据JsapiTicket

        public static String getJsapiTicket()
        {
            string ticket = string.Empty;//ticket
            try
            {
                //先看数据库中的ticket失效不，可用就用数据库中的，不可用就重新生成
                //数据库中获取最新ticketString
                Model.DD_GetToken model = new Model.DD_GetToken();
                model.ExecuteEL(41);
                if (model.ID > 0)
                {
                    string ticketString = model.Ticket;
                    Dictionary<string, object> json_Ticket = new Dictionary<string, object>();
                    JavaScriptSerializer js_Ticket = new JavaScriptSerializer();
                    js_Ticket.MaxJsonLength = int.MaxValue;
                    json_Ticket = js_Ticket.Deserialize<Dictionary<string, object>>(ticketString);

                    if (json_Ticket.Keys.Contains("ticket"))
                    {
                        ticket = json_Ticket["ticket"].ToString();
                    }
                }
                else  //如果数据库中的token和ticket失效则重新从钉钉接口获取
                {
                    //调用钉钉接口获取token
                    String url_Token = Env.OAPI_HOST + "/gettoken?" +
                        "corpid=" + Env.CORP_ID + "&corpsecret=" + Env.SECRET;
                    string tokenString = Request_url(url_Token, "GET", "");  //获取token的字符串

                    Dictionary<string, object> json_Token = new Dictionary<string, object>();
                    JavaScriptSerializer js_Token = new JavaScriptSerializer();
                    js_Token.MaxJsonLength = int.MaxValue;
                    json_Token = js_Token.Deserialize<Dictionary<string, object>>(tokenString);

                    if (json_Token.Keys.Contains("access_token"))
                    {
                        string token = json_Token["access_token"].ToString();
                        //调用钉钉接口获取ticket
                        String url_Ticket = Env.OAPI_HOST + "/get_jsapi_ticket?" +
                           "type=jsapi" + "&access_token=" + token;
                        string ticketString = Request_url(url_Ticket, "GET", "");

                        Dictionary<string, object> json_Ticket = new Dictionary<string, object>();
                        JavaScriptSerializer js_Ticket = new JavaScriptSerializer();
                        js_Ticket.MaxJsonLength = int.MaxValue;
                        json_Ticket = js_Ticket.Deserialize<Dictionary<string, object>>(ticketString);
                        if (json_Ticket.Keys.Contains("ticket"))
                        {
                            ticket = json_Ticket["ticket"].ToString();
                            //原数据已经过期了，将获取的最新token和ticket更新数据库
                            model.ExecNonQuery(31);//设置原来数据valid=0失效状态
                            model.Token = tokenString;
                            model.Ticket = ticketString;
                            model.Valid = 1;
                            model.ExecNonQuery(1);//添加数据
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return ticket;

        }



        //获取签名
        public static String sign(String ticket, String nonceStr, long timeStamp, String url)
        {
            String plain = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr +
                "&timestamp=" + timeStamp.ToString() + "&url=" + url;
            try
            {

                System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
                var encoder = System.Text.UTF8Encoding.UTF8;
                byte[] combined = encoder.GetBytes(plain);
                byte[] digest = hash.ComputeHash(combined);
                return bytesToHex(digest);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private static String bytesToHex(byte[] hash)
        {
            string str;
            string str2 = "";
            long num2 = hash.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                string str3 = Convert.ToByte(hash[i]).ToString("x");
                if (str3.Length == 1)
                {
                    str3 = "0" + str3;
                }
                str2 = str2 + str3;
            }
            str = str2;
            return str;
        }



        //public static String getConfig(String urlString, String queryString)
        //{
        //    String queryStringEncode = null;
        //    String url;
        //    if (queryString != null)
        //    {
        //        queryStringEncode = System.Web.HttpUtility.UrlDecode(queryString);
        //        url = urlString + "?" + queryStringEncode;
        //    }
        //    else
        //    {
        //        url = urlString;
        //    }

        //    String nonceStr = "abcdefg";
        //    long timeStamp = Convert.ToInt64(DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds) / 1000;
        //    String signedUrl = url;
        //    String accessToken = null;
        //    String ticket = null;
        //    String signature = null;
        //    try
        //    {
        //        accessToken = AuthHelper.getAccessToken();
        //        ticket = AuthHelper.getJsapiTicket(accessToken);
        //        signature = AuthHelper.sign(ticket, nonceStr, timeStamp, signedUrl);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return "{signature:'" + signature + "',nonceStr:'" + nonceStr + "',timeStamp:'" + timeStamp + "',corpId:'" + Env.CORP_ID + "'}";
        //}





        public static String Request_url(String url, string method, string postData)
        {
            String result = String.Empty;
            UriBuilder uri = new UriBuilder(url);
            HttpWebRequest http = HttpWebRequest.Create(uri.Uri) as HttpWebRequest;
            http.ServicePoint.Expect100Continue = false;
             http.UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.0)";
            //http.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
           // http.UserAgent = "Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 49.0.2623.110 Safari / 537.36 dingtalk - win / 1.0.0 nw(0.14.7) DingTalk(3.4.111 - RC.3) Mojo / 1.0.0";

            switch (method)
            {
                case "GET":
                    http.Method = "GET";
                    break;
                case "Post":
                    http.Method = "Post";
                    http.ContentType = "application/x-www-form-urlencoded";
                    using (StreamWriter sw = new StreamWriter(http.GetRequestStream()))
                    {
                        try
                        {
                            sw.Write(postData);
                        }
                        catch (IOException ex)
                        {
                            throw new Exception("post写入数据出错:" + ex.Message);
                        }
                    }
                    break;
            }
            try
            {
                using (HttpWebResponse response = http.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            catch (System.Net.WebException exc)
            {
                if (exc.Response != null)
                {
                    using (StreamReader sr = new StreamReader(exc.Response.GetResponseStream()))
                    {
                        String message = sr.ReadToEnd();
                        throw new Exception(message);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// post提交不能用
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public static string Request_url_Post(string url, string postdata)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(url);
               
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded;";
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabyte.Length;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //提交请求
                Stream stream;
                stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                //接收响应
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                //CookieCollection cook = response.Cookies;
                ////Cookie字符串格式
                //string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                Stream strm = response.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// post提交 可用的
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="param">参数，一般是json数据</param>
        /// <returns></returns>
        public static string Post(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }








        //封装好所有需要的参数，并且传递到企业应用网址的前端H5中。需要的参数有corpId,agentId,ticket,signature,nonceStr,timeStamp,url.
        //其中nonceStr,timeStamp,url用来在服务器后台生成signatrue签名，然后将ticket,nonceStr,timeStamp和signatrue传送到前台，前台网页就会
        //调用jsapi的dd.config函数重新生成signatrue, 和传进的signatrue进行比较，来实现验证过程。  
        /* 
        * 将所有需要传送到前端的参数进行打包，在前端会调用jsapi提供的dd.config接口进行签名的验证 
        *params: 
        *   request:在钉钉中点击微应用图标跳转的url地址 
        *return: 
        *   将需要的参数打包好，按json格式打包 
        */
        /// <summary>
        /// 获取config信息
        /// </summary>
        /// <returns></returns>
        public static String getConfig(string urlStr)
        {



            String corpId = Env.CORP_ID;        //一些比较重要的不变得参数本人存储在properties文件中  
            String corpSecret = Env.SECRET;      //公司钉钉上可以查到
            String nonceStr = "noncestr";       //随机数，任何字符串都可以
            String agentId = Env.agentId;     //agentid参数  微应用的agentid
            long timeStamp = Convert.ToInt64(DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds);     //时间戳参数  
            String url = urlStr; //请求链接的参数，这个链接主要用来生成signatrue，并不需要传到前端  
                                 // String accessToken = null;                              //token参数  
            String ticket = null;                                   //ticket参数  
            String signature = null;                                //签名参数  

            try
            {

                // accessToken = getAccessToken();
                //ticket = getJsapiTicket(accessToken);

                //  accessToken = "0a583a6c112a3c4c94831194fb7d9a0a";
                //  ticket = "5Wd0TLBiPtup8KrYycdvegGxdfYxXaBmr0YNttnPWJ5SAMBUyJOJBsfcX02lbEiX8Z2EI0zUGWl5zD3iIW6966";

                ticket = getJsapiTicket();//获取ticket

                signature = sign(ticket, nonceStr, timeStamp, url);//获取签名

            }
            catch (Exception e)
            {
                throw e;
            }
            return "{jsticket:'" + ticket + "',signature:'" + signature + "',nonceStr:'" + nonceStr + "',timeStamp:'"
            + timeStamp + "',corpId:'" + corpId + "',agentId:'" + agentId + "'}";
        }




    }
}