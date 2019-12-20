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
    public  interface IFriendsService
    {
        /// <summary>
        /// 加好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">好友session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddFriend(string session, string session_Friend);




        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="session">本人session</param>
        /// <returns>返回好友列表json</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetFriendList(string session);



        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">要删除的好友session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream CancelFriend(string session, string session_Friend);



        /// <summary>
        /// 搜索好友
        /// </summary>
        /// <param name="friendName">好友名称</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream SearchFriend(string session,string friendName);







    }
}
