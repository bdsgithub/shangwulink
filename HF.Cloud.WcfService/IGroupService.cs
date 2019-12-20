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
    public  interface IGroupService
    {

        /// <summary>
        /// 新建群组 修改群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="Introduce">群组简介</param>
        /// <param name="isOpen">1公开或0私密</param>
        /// <param name="groupID">群组ID 0添加 大于0修改</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream CreateGroup(string session, string groupName, string introduce,string isOpen,string groupID);



        /// <summary>
        /// 我的群组列表
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetMyGroups(string session);



        /// <summary>
        /// 群组人员列表
        /// </summary>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetGroupFriendList(string session,string groupID);




        /// <summary>
        /// 面对面加群(创建新群或加群)
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="password">口令</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream FaceToFaceCreateGroup(string session, string password, string lon, string lat);


        /// <summary>
        /// 搜索群
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="groupName">群名称</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream SearchGroup(string session, string groupName);


        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream IntoGroup(string session, string groupID);

        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream ExitGroup(string session, string groupID);


        /// <summary>
        /// 查看是否在群组中
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream IsInGroup(string session, string groupID);


        /// <summary>
        /// 获取群组小程序码和群组名称等内容
        /// </summary>
        /// <param name="path">小程序码跳转路径</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetQRCodeAndGroupDetail(string path, string groupID);


        /// <summary>
        /// 群组人员置顶 取消置顶  删除
        /// </summary>
        /// <param name="userID">人员ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="cookie">操作的功能（top置顶distop取消置顶delete删除）</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream EditUser(string session,string userID, string groupID, string cookie);





    }
}
