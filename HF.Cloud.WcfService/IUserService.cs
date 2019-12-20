using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.ServiceModel.Activation;

namespace HF.Cloud.WcfService
{
    [ServiceContract]
    public  interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "AddUser", Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Stream AddUser(Stream stream);

        /// <summary>
        /// 上传头像图片
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "UploadUserIcon", Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Stream UploadUserIcon(Stream stream);

        /// <summary>
        /// 收取手机验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetValidateCode(string phone);

        /// <summary>
        /// 验证验证码是否正确是否过期
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream IsValidateCodeTrue(string phone,string code);






        /// <summary>
        /// 获取手机号
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="encryptedData">用户信息的加密数据</param>
        /// <param name="iv">加密算法的初始向量</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)] 
        Stream GetPhoneNumber(string session, string encryptedData,string iv);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetUserInfoBySession(string session);






        /// <summary>
        /// 获取用户综合信息
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="sessionFriend">需要获取信息的用户session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetUserInfoBySessionAndSessionFriend(string session, string sessionFriend);




        /// <summary>
        /// 通过code获取session
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetSessionByCode(string code);


        /// <summary>
        /// 获取个人小程序码图片
        /// </summary>
        /// <param name="path">小程序码跳转的路径</param>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetUserQRCode(string path,string session);




    }
}
