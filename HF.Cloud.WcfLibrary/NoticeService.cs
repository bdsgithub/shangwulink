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
    public class NoticeService : INoticeService
    {
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        public Stream GetNotice(string session,string noticeType)
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.GetNotice(session,noticeType);

                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetNotice Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 获取未查看通知个数
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        public Stream NoLookNoticeNumber(string session)
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.NoLookNoticeNumber(session);

                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("NoLookNoticeNumber Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 通知忽略
        /// </summary>
        /// <param name="noticeID">通知ID</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        public Stream NoticeHuLue(string session, string noticeID,string noticeType)
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.NoticeHuLue(session,noticeID,noticeType);

                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("NoticeHuLue Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }






        /// <summary>
        /// 通知列表中同意添加好友
        /// </summary>
        /// <param name="session">本人的session</param>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="sessionFriend">好友的session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        public Stream AddFriendByNotice(string session, string noticeID, string sessionFriend,string noticeType)
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.AddFriendByNotice(session,noticeID, sessionFriend, noticeType);

                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("AddFriendByNotice Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 更改通知状态为已经查看
        /// </summary>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="noticeType">通知的类型0个人，1公司，2系统</param>
        /// <returns></returns>
        public Stream UpdateNoticeLooked(string noticeID, string noticeType)
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.UpdateNoticeLooked(noticeID, noticeType);

                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("UpdateNoticeLooked Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


         /// <summary>
        /// 获取图片路径列表
        /// </summary>
        /// <returns></returns>
         public Stream GetSystemPicture()
        {
            try
            {
                NoticeBLL noticeBLL = new NoticeBLL();
                string resul = noticeBLL.GetSystemPicture();
                if (!string.IsNullOrEmpty(resul))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resul));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetSystemPicture Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }
        

    }
}
