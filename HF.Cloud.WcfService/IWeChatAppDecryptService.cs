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
    public  interface IWeChatAppDecryptService
    {
        /// <summary>
        /// 通过code获取微信openid
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetOpenidAndSessionKeyByCode(string code); 
    }
}
