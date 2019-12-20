using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace HF.Cloud.WcfService
{
    [ServiceContract]
    public  interface INoticeService
    {
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetNotice(string session,string noticeType);


         /// <summary>
        /// 获取未查看通知个数
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream NoLookNoticeNumber(string session);


        /// <summary>
        /// 通知忽略
        /// </summary>
        /// <param name="noticeID">通知ID</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream NoticeHuLue(string session, string noticeID,string noticeType);


        /// <summary>
        /// 通知列表中同意添加好友
        /// </summary>
        /// <param name="session">本人的session</param>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="sessionFriend">好友的session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddFriendByNotice(string session, string noticeID, string sessionFriend,string noticeType);


        /// <summary>
        /// 更改通知状态为已经查看
        /// </summary>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="noticeType">通知的类型0个人，1公司，2系统</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream UpdateNoticeLooked(string noticeID, string noticeType);


        /// <summary>
        /// 获取图片路径列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetSystemPicture();




    }
}
