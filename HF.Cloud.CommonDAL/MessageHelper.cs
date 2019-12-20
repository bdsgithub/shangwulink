using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace HF.Cloud.CommonDAL
{
    public class MessageHelper
    {
        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="Validatecode"></param>
        /// <param name="messageCode"></param>
        public void SendValidateMessage(string phone, string Validatecode, string messageCode)
        {
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init(ConfigHelper.GetConfigString("Madd"), ConfigHelper.GetConfigString("Mport"));
            api.setAccount(ConfigHelper.GetConfigString("Maccount"), ConfigHelper.GetConfigString("Mtoken"));
            api.setAppId(ConfigHelper.GetConfigString("Mappid"));

            if (isInit)
            {
                string[] strmessage = new string[] {
                    Validatecode,
                    ConfigHelper.GetConfigString("Mpasstime")
                };
                Dictionary<string, object> retData = api.SendTemplateSMS(phone, messageCode, strmessage);
                string ret = getDictionaryData(retData);
            }
            else
            {
                //ret = "初始化失败";
            }

        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="messageCode"></param>
        /// <param name="message"></param>
        public void SendMessage(string phone, string messageCode, string[] message)
        {
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init(ConfigHelper.GetConfigString("Madd"), ConfigHelper.GetConfigString("Mport"));
            api.setAccount(ConfigHelper.GetConfigString("Maccount"), ConfigHelper.GetConfigString("Mtoken"));
            api.setAppId(ConfigHelper.GetConfigString("Mappid"));

            if (isInit)
            {
                //string[] strmessage = new string[] {
                //    Validatecode,
                //    ConfigHelper.GetConfigString("Mpasstime")
                //};
                Dictionary<string, object> retData = api.SendTemplateSMS(phone, messageCode, message);
                string ret = getDictionaryData(retData);
            }
            else
            {
                //ret = "初始化失败";
            }

        }
    }
}
