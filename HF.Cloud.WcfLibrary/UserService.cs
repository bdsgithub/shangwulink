using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using HF.Cloud.WcfService;
using HF.Cloud.BLL;
using HF.Cloud.BLL.Common;
using System.ServiceModel.Activation;

namespace HF.Cloud.WcfLibrary
{
    [AspNetCompatibilityRequirements( RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Stream AddUser(Stream stream)
        {
            string Session_True = "";
            if (stream != null)
            {
                StreamReader sr = new StreamReader(stream);
                string s = sr.ReadToEnd();
                sr.Dispose();

                UserBLL bl = new UserBLL();
                try
                {
                    Session_True = bl.InsertUser(s);
                }
                catch (Exception err)
                {

                    HF.Cloud.BLL.Common.Logger.Error("AddUser Error", err);
                }
            }
            if (Session_True != "")
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(Session_True));
            }
            else
            {
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 上传头像图片
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Stream UploadUserIcon(Stream stream)
        {
            string Session_True = "";
            if (stream != null)
            {
                //StreamReader sr = new StreamReader(stream);
                //string s = sr.ReadToEnd();
                //sr.Dispose();

                UserBLL bl = new UserBLL();
                try
                {
                    Session_True = bl.UploadUserIcon(stream);
                }
                catch (Exception err)
                {

                    HF.Cloud.BLL.Common.Logger.Error("UploadImg Error", err);
                }
            }
            if (Session_True != "")
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(Session_True));
            }
            else
            {
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 获取短信验证码
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

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Stream IsValidateCodeTrue(string phone, string code)
        {
            try
            {
                RegistUserHandler regist = new RegistUserHandler();
                string number = regist.IsValidateCodeTrue(phone,code);

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
                Logger.Error("IsValidateCodeTrue Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }





        /// <summary>
        /// 获取手机号
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="encryptedData">用户信息的加密数据</param>
        /// <param name="iv">加密算法的初始向量</param>
        /// <returns></returns>
        public Stream GetPhoneNumber(string session, string encryptedData, string iv)
        {
            try
            {
                WeChatAppDecrypt wcad = new WeChatAppDecrypt();
                string resul = wcad.GetPhoneNumber(session, encryptedData,iv);
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetPhoneNumber Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public Stream GetUserInfoBySession(string session)
        {
            try
            {
                UserBLL ub = new UserBLL();
                string resul = ub.GetUserInfoBySession(session);
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetUserInfoBySession Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }




        /// <summary>
        /// 获取用户综合信息
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="sessionFriend">需要获取信息的用户session</param>
        /// <returns></returns>
        public Stream GetUserInfoBySessionAndSessionFriend(string session, string sessionFriend)
        {
            try
            {
                UserBLL ub = new UserBLL();
                string resul = ub.GetUserInfoBySessionAndSessionFriend(session,sessionFriend);
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetUserInfoBySessionAndSessionFriend Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 通过code获取session
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Stream GetSessionByCode(string code)
        {
            try
            {
                UserBLL ub = new UserBLL();
                string resul = ub.GetSessionByCode(code);
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetSessionByCode Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 获取个人小程序码图片
        /// </summary>
        /// <param name="path">小程序码跳转的路径</param>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public Stream GetUserQRCode(string path,string session)
        {
            try
            {
                UserBLL ub = new UserBLL();
                string resul = ub.GetUserQRCode(path,session);
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetUserQRCode Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }








    }
}
