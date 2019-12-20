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
   public interface ILoginService
    {
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        string GetData(string str);


        /// <summary>
        /// 收取手机验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetValidateCode(string phone);



    }
}
