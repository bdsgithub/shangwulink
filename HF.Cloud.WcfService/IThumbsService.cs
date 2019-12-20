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
    public interface IThumbsService
    {
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="session">被点赞人的session</param>
        /// <param name="session_Thumb">点赞人的session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream AddThumb(string session, string session_Thumb);


        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="session">被点赞人的session</param>
        /// <param name="session_Thumb">点赞人的session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream CancelThumb(string session, string session_Thumb);


        /// <summary>
        /// 获取点赞个数
        /// </summary>
        /// <param name="session">要查询的用户session</param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Stream GetThumbs(string session);






    }
}
