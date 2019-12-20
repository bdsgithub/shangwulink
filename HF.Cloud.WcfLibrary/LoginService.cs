using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HF.Cloud.WcfService;
using HF.Cloud.BLL;
using HF.Cloud.BLL.Common;

namespace HF.Cloud.WcfLibrary
{

    public class LoginService : ILoginService
    {
        public string GetData(string str)
        {
            string strT = "";
            HF.Cloud.BLL.Common.Logger.Error(str);
            if (str != null)
            {
                try
                {
                    strT = "你好" + str;
                }
                catch (Exception err)
                {
                    HF.Cloud.BLL.Common.Logger.Error("AssetBind Error", err);
                }
            }
            if (strT.Length > 2)
            {
                return strT;
            }
            else
            {
                return "error";
            }
        }
        /// <summary>
        /// APP获取短信验证码
        /// </summary>
        /// <param name="phone">用户手机号（账号）</param>
        /// <returns></returns>
        public Stream GetValidateCode(string phone)
        {
            try
            {
                RegistUserHandler regist = new RegistUserHandler();
                string number = regist.SendMessageForLogin(phone);

                if (!string.IsNullOrEmpty(number))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(number));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetValidateCode Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }










    }
}
