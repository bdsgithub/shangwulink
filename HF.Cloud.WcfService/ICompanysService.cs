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
    public  interface ICompanysService
    {
        /// <summary>
        /// 添加公司或查询获取公司ID
        /// </summary>
        /// <param name="companyName">公司名称</param>
        /// <returns>返回值是公司ID</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddOrSelectCompany(string companyName);

        /// <summary>
        /// 公司页面，添加或选择公司
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="companyName">公司全名称</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddOrSelectCompany_EditCompany(string session, string companyName);



        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddCompanyTag(string companyID, string tag);



        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)] 
        Stream CancelCompanyTag(string companyID, string tag);




        /// <summary>
        /// 获取公司关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetCompanyTag(string companyID);



        /// <summary>
        /// 获取本人公司信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetMyCompanyInfo(string session);


        /// <summary>
        /// 获取好友公司信息
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetFriendCompanyInfo(string session, string companyID);



        /// <summary>
        /// 给公司递名片
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream SendCard(string session,string companyID);






        /// <summary>
        /// 模糊搜索公司
        /// </summary>
        /// <param name="keyword">公司关键字</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream SearchCompanyName(string keyword);


        /// <summary>
        /// 修改公司简介
        /// </summary>
        /// <param name="companyContent">公司内容</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateCompanyIntroduce", Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Stream UpdateCompanyIntroduce(Stream stream);


    






    }
}
