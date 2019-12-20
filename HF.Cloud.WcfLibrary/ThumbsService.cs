using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using HF.Cloud.WcfService;
using HF.Cloud.BLL;
using HF.Cloud.BLL.Common;

namespace HF.Cloud.WcfLibrary
{
    public class ThumbsService : IThumbsService
    {
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="session">被点赞人的session</param>
        /// <param name="session_Thumb">点赞人的session</param>
        /// <returns></returns>
        public Stream AddThumb(string session, string session_Thumb)
        {
            try
            {
                ThumbsBLL thum = new ThumbsBLL();
                string number = thum.AddThumb(session, session_Thumb);

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
                Logger.Error("AddThumb Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="session">被点赞人的session</param>
        /// <param name="session_Thumb">点赞人的session</param>
        /// <returns></returns>
        public Stream CancelThumb(string session, string session_Thumb)
        {
            try
            {
                ThumbsBLL thum = new ThumbsBLL();
                string number = thum.CancelThumb(session, session_Thumb);

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
                Logger.Error("AddThumb Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 获取点赞个数
        /// </summary>
        /// <param name="session">要查询的用户session</param>
        /// <returns></returns>
        public Stream GetThumbs(string session)
        {
            try
            {
                ThumbsBLL thum = new ThumbsBLL();
                string number = thum.GetThumbs(session);

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
                Logger.Error("AddThumb Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }















    }
}
