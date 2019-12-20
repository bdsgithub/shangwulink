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
    public class GroupService : IGroupService
    {
        /// <summary>
        /// 新建群组  修改群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="Introduce">群组简介</param>
        /// <param name="isOpen">1公开或0私密</param>
        /// <param name="groupID">群组ID 0添加  大于0修改</param>
        /// <returns></returns>
        public Stream CreateGroup(string session, string groupName, string introduce,string isOpen,string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.CreateGroup(session, groupName, introduce,isOpen, groupID);
                if(!string.IsNullOrEmpty(resul))
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
                Logger.Error("CreateGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 我的群组列表
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        public Stream GetMyGroups(string session)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.GetMyGroups(session);

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
                Logger.Error("GetMyGroups Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }




        /// <summary>
        /// 群组人员列表
        /// </summary>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public Stream GetGroupFriendList(string session,string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.GetGroupFriendList(session,groupID);

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
                Logger.Error("GetGroupFriendList Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 面对面加群(创建新群或加群)
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="password">口令</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public Stream FaceToFaceCreateGroup(string session, string password, string lon, string lat)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.FaceToFaceCreateGroup(session, password, lon,lat);

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
                Logger.Error("FaceToFaceCreateGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }




        /// <summary>
        /// 搜索群
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="groupName">群名称</param>
        /// <returns></returns>
        public Stream SearchGroup(string session, string groupName)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.SearchGroup(session, groupName);

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
                Logger.Error("SearchGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public Stream IntoGroup(string session, string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.IntoGroup(session, groupID);

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
                Logger.Error("IntoGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }





        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public Stream ExitGroup(string session, string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.ExitGroup(session, groupID);

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
                Logger.Error("ExitGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 查看是否在群组中
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public Stream IsInGroup(string session, string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.IsInGroup(session, groupID);

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
                Logger.Error("IsInGroup Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }




        /// <summary>
        /// 获取群组小程序码和群组名称等内容
        /// </summary>
        /// <param name="path">小程序码跳转路径</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public Stream GetQRCodeAndGroupDetail(string path, string groupID)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.GetQRCodeAndGroupDetail(path, groupID);

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
                Logger.Error("GetQRCodeAndGroupDetail Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }



        /// <summary>
        /// 群组人员置顶 取消置顶  删除
        /// </summary>
        /// <param name="userID">人员ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="cookie">操作的功能（top置顶distop取消置顶delete删除）</param>
        /// <returns></returns>
        public Stream EditUser(string session,string userID, string groupID, string cookie)
        {
            try
            {
                GroupBLL groupBLL = new GroupBLL();
                string resul = groupBLL.EditUser(session,userID, groupID,cookie);

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
                Logger.Error("EditUser Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }
    }
}
