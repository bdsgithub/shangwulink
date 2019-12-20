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
    public interface IHongBaoService
    {
        /// <summary>
        /// 红包开关
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream OpenRedBag(string companyID);



        /// <summary>
        /// 领红包
        /// </summary>
        /// <param name="session">用户sesison</param>
        /// <param name="companyID">发红包的公司ID</param>
        /// <param name="formID">用户提交submit表单的formID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetRedBag(string session, string companyID, string formID);

        /// <summary>
        /// 是否激活红包
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream IsActive(string companyID);


    }
}
