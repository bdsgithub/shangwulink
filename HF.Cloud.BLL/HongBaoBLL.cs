using HF.Cloud.CommonDAL;
using HF.Cloud.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace HF.Cloud.BLL
{
   public class HongBaoBLL
    {

        /// <summary>
        /// 红包开关
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public string OpenRedBag(string companyID)
        {
            HF.Cloud.BLL.Common.Logger.Error("OpenRedBag 获取到的参数CompanyID："+companyID);
            //查看时间，过期则不可打开
            //if(DateTime.Now.ToString()<"2018-02-16 23:12:12:123")
            //{
            //}
            string resultStr = "";
            long companyIdLong = 0;//公司ID
            if (long.TryParse(companyID, out companyIdLong))
            {
                //1通过公司id在用户表中看当前公司够5个人不够
                SB_UserEL userEL = new SB_UserEL();
                userEL.CompanyID = companyIdLong;
                DataTable dt_User = userEL.ExecDT(44);
                //1.1够5个人则开始在红包表里面添加红包数据
                if(dt_User != null && dt_User.Rows.Count >= 5)
                {
                    //2 查看已经发了多少钱，发过的总数大于1000停止发送
                    DataTable dt_HongBalAllVa=DbHelperSQL.Query("select sum(HongBaoValue) as AllValue from T_HongBao where Valid=1").Tables[0];
                        string allValueStr = dt_HongBalAllVa.Rows[0][0].ToString();
                        //2.1已经发送的总金额小于900的话继续发
                        if(string.IsNullOrEmpty(allValueStr)||double.Parse(allValueStr)< 850)
                        {
                            T_HongBaoEL hongBaoEl = new T_HongBaoEL();
                            //查看红包表是否已经存在记录，存在则不添加红包记录
                            hongBaoEl.CompanyID = companyIdLong;
                            DataTable dt_HongBao = hongBaoEl.ExecDT(33);
                            if (dt_HongBao == null|| dt_HongBao.Rows.Count<1)
                            {
                                //3发红包表添加记录
                                //3.1生成公司红包金额10-100随机数，红包个数是金额的0.5-1倍
                                Random ran = new Random();
                                //3.1.1红包金额,取下不取上
                                int comAllValue = ran.Next(10,100);
                                //3.1.2人数
                                int comNumber = (int)Math.Floor(ran.Next(5, 10) * 0.1 * comAllValue);
                                HF.Cloud.BLL.Common.Logger.Error("OpenRedBag方法红包总金额是：" + comAllValue + "---总人数：" + comNumber);
                                //3.2通过总金额和人数获取评分后的红包值
                                List<Double> listHongV = HongBao.GetRedBagList(double.Parse(comAllValue.ToString()), comNumber, 1.00);
                                //3.3通过获取到的红包值列表，给红包表添加记录
                                for(int i = 0; i < listHongV.Count; i++)
                                {
                                    //ID,CompanyID,HongBaoValue,GetUserID,IsGet,
                                    //CreateTime,GiveOutTime,Valid
                                    //HF.Cloud.BLL.Common.Logger.Error("OpenRedBag方法获得的红包值listHongV是：" + listHongV[i]);
                                    hongBaoEl.CompanyID = companyIdLong;
                                    //hongBaoEl.HongBaoValue = decimal.Round((decimal)listHongV[i],2);
                                    hongBaoEl.HongBaoValue = decimal.Parse(listHongV[i].ToString());
                                    hongBaoEl.IsGet = 1;
                                    hongBaoEl.CreateTime = DateTime.Now.ToString();
                                    hongBaoEl.GiveOutTime = "";
                                    hongBaoEl.Valid = 1;
                                    int ra;
                                    hongBaoEl.ExecNonQuery(1, out ra);
                                }
                                HF.Cloud.BLL.Common.Logger.Error("OpenRedBag方法生成的红包个数：" + listHongV.Count);
                                resultStr = "success";
                            }
                            else//已经激活不能再次激活
                            {
                                resultStr = "actived";
                            }
                        }
                        else//2.2红包奖金池已经用完额度
                        {
                            resultStr = "empty";
                        }
                }
                else//1.2不够5个人则提示人数不够
                {
                    resultStr = "notenough";
                }
            }
            else
            {
                resultStr = "error";
            }
            HF.Cloud.BLL.Common.Logger.Error("OpenRedBag 结果：" + resultStr);
            return resultStr;
        }



        /// <summary>
        /// 领红包
        /// </summary>
        /// <param name="session">用户sesison</param>
        /// <param name="companyID">发红包的公司ID</param>
        ///<param name="formID">用户提交submit表单的formID</param>
        /// <returns></returns>
        public string GetRedBag(string session,string companyID,string formID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetRedBag获取的参数session：" + session + "---companyID"+ companyID + "---formID" + formID);
            string   resultStr = "";
            //查看当前公司有没有红包记录
            long companyIdLong = 0;//公司ID
            if (long.TryParse(companyID, out companyIdLong)&&!string.IsNullOrEmpty(companyID)&&!string.IsNullOrEmpty(session))
            {
                UserBLL userBLL = new UserBLL();
                //通过session获取UserID
                // long userID = userBLL.GetUserIDBySession(session);

                T_HongBaoEL honBaoEL = new T_HongBaoEL();
                honBaoEL.CompanyID = companyIdLong;
                //查看是否领取过当前公司的红包
                honBaoEL.GetUserSession_True = session.Substring(22,28);//只获取openid，只有openID是唯一的
                DataTable dt_GetHongBao = honBaoEL.ExecDT(32); 
                //如果领取过就返回领取的金额(不能再次领取)
                if(dt_GetHongBao!=null&&dt_GetHongBao.Rows.Count>0)
                {
                    resultStr = dt_GetHongBao.Rows[0]["HongBaoValue"].ToString();
                }
                else//没有记录的话就领取红包
                {
                    //获取当前公司未领取的红包
                    DataTable dt_HongBao = honBaoEL.ExecDT(31);
                    if (dt_HongBao != null && dt_HongBao.Rows.Count > 0)
                    {
                        //获取红包值 
                        decimal hongBaoValueDecimal = (decimal)dt_HongBao.Rows[0]["HongBaoValue"];
                        decimal dt_decimal_100 = hongBaoValueDecimal * 100;
                        int dt_decimal_int = Decimal.ToInt32(dt_decimal_100);
                        int sHonBao = dt_decimal_int;
                        HF.Cloud.BLL.Common.Logger.Error("GetRedBag获取的红包值hongBaoValueDecimal：" + hongBaoValueDecimal);

                        //修改当前的红包记录的状态为已经领取
                        honBaoEL.ID = (long)dt_HongBao.Rows[0]["ID"];
                        honBaoEL.GetUserSession_True = session.Substring(22,28);//只获取openid，只有openID是唯一的
                        honBaoEL.IsGet = 0;
                        honBaoEL.GiveOutTime = DateTime.Now.ToString();
                        int ra;
                        long exResult = honBaoEL.ExecNonQuery(21, out ra);
                        if (ra > 0)
                        {
                            //从session中解析openid
                            string str_openid = session.Substring(22, 28);
                            //调用接口发红包
                            string sendResult = SendRedBag(formID,str_openid, hongBaoValueDecimal, companyID);
                            if (sendResult == "success")//红包发送成功
                            {
                                resultStr = hongBaoValueDecimal.ToString();
                            }
                            else
                            { resultStr = "error"; }
                        }
                        else
                        { resultStr = "error"; }
                    }
                    else//红包已经领完了
                    {
                        resultStr = "null";
                    }
                }
            }
            return resultStr;
        }

        /// <summary>
        /// 是否激活红包
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public  string IsActive(string companyID)
        {
            HF.Cloud.BLL.Common.Logger.Error("IsActive获取的参数companyID：" + companyID);
            string resultStr = "";
            //查看当前公司有没有红包记录
            long companyIdLong = 0;//公司ID
            if (long.TryParse(companyID, out companyIdLong) && !string.IsNullOrEmpty(companyID))
            {
                //查看当前公司是否有红包记录
                T_HongBaoEL honBaoEL = new T_HongBaoEL();
                honBaoEL.CompanyID = companyIdLong;
                DataTable dt_HongBao = honBaoEL.ExecDT(33);
                if (dt_HongBao != null && dt_HongBao.Rows.Count > 0)
                {
                    resultStr = "have";
                }
                else
                {
                    resultStr = "no";
                }
            }
            else
            {
                resultStr = "error";
            }
           return resultStr;
        }



        //企业付款到用户零钱
        public string SendRedBag(string formID,string openidStr,decimal hongBaoValueDecimal,string companyID)
        {
            string strReturn = "";
            BLL.Common.Logger.Error("SendRedBag方法获取的参数openidStr：" + openidStr+ "---formID:"+ formID + "---hongBaoValueDecimal:" + hongBaoValueDecimal);
            string mch_appid = "wx7665d52714005c15";//小程序的appid
            string mchid = "1388364402";//商户号,安装证书密码也是此号
            string device_info = "20180124";//设备号
            string nonce_str = Guid.NewGuid().ToString().Replace("-", "");//随机字符串

            string partner_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss");//商户订单号
            //获取用户openid
            string openid = openidStr;
            //校验用户姓名选项 NO_CHECK：不校验真实姓名  FORCE_CHECK：强校验真实姓名
            string check_name = "NO_CHECK";
            //红包值改为分
            decimal dt_decimal_100 = hongBaoValueDecimal * 100;
            int dt_decimal_int = Decimal.ToInt32(dt_decimal_100);
            int amount = dt_decimal_int;//企业付款金额，单位为分
            string desc = "新春红包";//企业付款操作说明信息。必填。
            string spbill_create_ip = "123.56.153.172";//Ip地址

            // 第一步，设所有发送或者接收到的数据为集合M，将集合M内非空参数值的参数按照参数名ASCII码从小到大排序（字典序）
            //使用URL键值对的格式（即key1 = value1 & key2 = value2…）拼接成字符串stringA。 

            //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
            SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
            m_values.Add("mch_appid", mch_appid);
            m_values.Add("mchid", mchid);
            m_values.Add("device_info", device_info);
            m_values.Add("nonce_str", nonce_str);
            m_values.Add("partner_trade_no", partner_trade_no);
            m_values.Add("openid", openid);
            m_values.Add("check_name", check_name);
            m_values.Add("amount", amount);
            m_values.Add("desc", desc);
            m_values.Add("spbill_create_ip", spbill_create_ip);
            
            //第一步：对参数按照key = value的格式，并按照参数名ASCII字典序排序如下：
            string stringA = ToUrl(m_values);
            //第二步：拼接API密钥： 
            //stringSignTemp = "stringA&key=192006250b4c09247ec02edce69f6a2d"
            //sign = MD5(stringSignTemp).toUpperCase() = "9A0A8659F005D6984697E2CA0A9CF3B7"
            string apiKey = "ZhengZhou2YiQiShang0XingXiKeJi17";//API安全-->密钥
            string sign = MakeSign(stringA, apiKey);
            //最终得到最终发送的数据： 
            m_values.Add("sign", sign);//获取sign后再添加sign
            string xml = ToXml(m_values);//获取xml文件
            BLL.Common.Logger.Error("提交的xml数据：" + xml);

            try
            {
                //系统必须已经导入cert指向的证书
                string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
                //post提交
                string result = apiPost(xml, url);
                BLL.Common.Logger.Error("发红包结果：" + result);
                //xml转换成字典
                SortedDictionary<string, object> resultDictionary = FromXml(result);
                BLL.Common.Logger.Error("return_msg数据：" + resultDictionary["return_msg"].ToString());
                if(resultDictionary["return_code"].ToString() == "SUCCESS" && resultDictionary["result_code"].ToString() == "SUCCESS")
                {
                    //发送模板消息
                    SendServNotice(openidStr, formID, hongBaoValueDecimal,companyID);
                    strReturn = "success";
                    BLL.Common.Logger.Error("充值记录中添加记录结果，大于0成功：");
                }
                else
                { strReturn = resultDictionary["return_msg"].ToString(); }
            }
            catch (Exception exp)
            {
                BLL.Common.Logger.Error("post提交发送错误：" + exp.ToString());
            }
            
            return strReturn;
        }


        /// <summary>
        /// 发送微信服务通知
        /// </summary>
        /// <param name="userOpenID">发送目的用户的openid</param>
        /// <param name="formID">提交表单的formid</param>
        /// <param name="hongBaoValueDecimal">红包值</param>
        public void SendServNotice(string userOpenID,string formID,decimal hongBaoValueDecimal,string companyID)
        {
            //通过公司id获取公司名称
            string companyName = "公司";
            if(!string.IsNullOrEmpty(companyID))
            {
                CompanysEL companyEL = new CompanysEL();
                companyEL.ID = long.Parse(companyID);
                DataTable dt_company = companyEL.ExecDT(2);
                if(dt_company!=null&&dt_company.Rows.Count>0)
                {
                   companyName = dt_company.Rows[0]["CompanyName"].ToString();
                }
            }
           
            SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
            m_values.Add("touser", userOpenID);//接收者（用户）的 openid
            m_values.Add("template_id", "2bqqDRenEIyb5D0V8ngm7BYn0tukqGdOFEYx5m2-z28");//所需下发的模板消息的id(小程序后台配置)
            m_values.Add("page", "pages/mycard/mycard");//点击模板卡片后的跳转页面
            m_values.Add("form_id", formID);//表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id

            SortedDictionary<string, object> m_values_data = new SortedDictionary<string, object>();
            SortedDictionary<string, object> m_values_data_Item1 = new SortedDictionary<string, object>();
            m_values_data_Item1.Add("value", "迎春纳福 喜送红包");//
           // m_values_data_Item1.Add("color", "#173177");//
            m_values_data.Add("keyword1", m_values_data_Item1);

            SortedDictionary<string, object> m_values_data_Item2 = new SortedDictionary<string, object>();
            m_values_data_Item2.Add("value", hongBaoValueDecimal + "元现金红包");//
            m_values_data_Item2.Add("color", "#F70909");
            m_values_data.Add("keyword2", m_values_data_Item2);

            SortedDictionary<string, object> m_values_data_Item3 = new SortedDictionary<string, object>();
            m_values_data_Item3.Add("value","您已经成功领取"+ companyName + "送出的"+ hongBaoValueDecimal + "元新春红包。\n点击进入小程序，免费开启自己企业的新春红包，为好友送祝福吧！");//
            //m_values_data_Item3.Add("color", "#F70909");
            m_values_data.Add("keyword3", m_values_data_Item3);

            m_values.Add("data", m_values_data);//模板内容
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            string json = js.Serialize(m_values);
            BLL.Common.Logger.Error("发模板消息提交的json数据：" + json);


            //string xml = ToXml(m_values);//获取xml文件
            //BLL.Common.Logger.Error("发模板消息提交的xml数据：" + xml);

            try
            {
                //获取access_token
                WX_TokenBLL tokenBLL = new WX_TokenBLL();
                string access_token = tokenBLL.GetToken();
                //发送模板消息 接口地址
                string url = "https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token="+ access_token;
                //post提交
                //string result = apiPost(xml, url);
                string result =Post(url,json);
                BLL.Common.Logger.Error("发模板消息结果：" + result);
                //xml转换成字典
                //SortedDictionary<string, object> resultDictionary = FromXml(result);
                //BLL.Common.Logger.Error("errmsg数据：" + resultDictionary["errmsg"].ToString());
                //if (resultDictionary["errmsg"].ToString() == "ok")
                //{
                //    BLL.Common.Logger.Error("发送模板成功！");
                //}
                Dictionary<string, object> result_Dic  = new Dictionary<string, object>();
                result_Dic=js.Deserialize<Dictionary<string, object>>(result);
                if (result_Dic.Keys.Contains("errcode"))
                {
                    if (result_Dic["errcode"].ToString() == "0")
                    {
                        BLL.Common.Logger.Error("发送模板成功！");
                    }
                }
            }
            catch (Exception exp)
            {
                BLL.Common.Logger.Error("发送模板错误：" + exp.ToString());
            }
        }














        /**
     * @Dictionary格式转化成url参数格式
     * @ return url格式串, 该串不包含sign字段值
     */
        public string ToUrl(SortedDictionary<string, object> m_values)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    //throw new WxPayException("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }
        /**
  * @生成签名，详见签名生成算法
  * @return 签名, sign字段不参加签名
*/
        public string MakeSign(string stringA, string key)
        {

            //在string后加入API KEY
            stringA += "&key=" + key;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(stringA));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /**
     * @将Dictionary转成xml
     * @return 经转换得到的xml串
     * @throws WxPayException
     **/
        public string ToXml(SortedDictionary<string, object> m_values)
        {
            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                //Log.Error(this.GetType().ToString(), "WxPayData数据为空!");
                //throw new WxPayException("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    //throw new WxPayException("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    // Log.Error(this.GetType().ToString(), "WxPayData字段数据类型错误!");
                    //throw new WxPayException("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }
        public string apiPost(string xml, string url)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                // request.Timeout = timeout * 1000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                ////是否使用证书
                X509Store store = new X509Store("My", StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                System.Security.Cryptography.X509Certificates.X509Certificate2 cert =
                store.Certificates.Find(X509FindType.FindBySubjectName, "河南易企尚信息科技有限公司", false)[0];
                request.ClientCertificates.Add(cert);
                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                BLL.Common.Logger.Error("抛出异常错误：" + e.ToString());
            }
            catch (Exception e)
            {
                BLL.Common.Logger.Error("抛出异常错误：" + e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        /**
* @将xml转为WxPayData对象并返回对象内部的数据
* @param string 待转换的xml串
* @return 经转换得到的Dictionary
* @throws WxPayException
*/
        public SortedDictionary<string, object> FromXml(string xml)
        {
            SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
            if (string.IsNullOrEmpty(xml))
            {
                // Log.Error(this.GetType().ToString(), "将空的xml串转换为WxPayData不合法!");
                //throw new WxPayException("将空的xml串转换为WxPayData不合法!");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"].ToString() != "SUCCESS")
                {
                    return m_values;
                }
                // CheckSign();//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return m_values;
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














    }
}
