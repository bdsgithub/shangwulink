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
    public class FriendsService : IFriendsService
    {
        /// <summary>
        /// 加好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">好友session</param>
        /// <returns></returns>
        public Stream AddFriend(string session, string session_Friend)
        {
            try
            {
                FriendsBLL friBLL = new FriendsBLL();
                string resul = friBLL.AddFriend(session, session_Friend);

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
                Logger.Error("AddFriend Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }
        
        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="session">本人session</param>
        /// <returns></returns>
        public Stream GetFriendList(string session)
        {
            try
            {
                FriendsBLL friBLL = new FriendsBLL();
                string resul = friBLL.GetFriendList(session);

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
                Logger.Error("GetFriendList Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">要删除的好友session</param>
        /// <returns></returns>
        public Stream CancelFriend(string session, string session_Friend)
        {
            try
            {
                FriendsBLL friBLL = new FriendsBLL();
                string resul = friBLL.CancelFriend(session, session_Friend);

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
                Logger.Error("CancelFriend Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 搜索好友
        /// </summary>
        /// <param name="friendName">好友名称</param>
        /// <returns></returns>
        public Stream SearchFriend(string session,string friendName)
        {
            try
            {
                FriendsBLL friBLL = new FriendsBLL();
                string resul = friBLL.SearchFriend(session,friendName);

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
                Logger.Error("SearchFriend Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }













    }
}
