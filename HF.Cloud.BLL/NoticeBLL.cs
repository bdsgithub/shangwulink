using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.Model;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


namespace HF.Cloud.BLL
{
   public class NoticeBLL
    {
        /// <summary>
        /// 获取通知列表
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        public string GetNotice(string session,string noticeType)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到参数session:" + session);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //通过session获取用户ID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到userID:" + userID);
            NoticeEL noticeEL = new NoticeEL();
            noticeEL.UserID = userID;
            DataTable dt = new DataTable();
            if (noticeType == "0")//全部通知
            {
                string sqlString = "select* from (" +
                "select N.ID as NID, S.ID as SID, N.UserID_Friend,N.GroupID," +
                "ISNULL(N.UserID, S.UserID) as UserID," +
                "ISNULL(N.NoticeType, S.NoticeType) as NoticeType," +
                "N.NoticeState, S.NoticeTitle, S.NoticeContent, s.AddressUrl," +
                "ISNULL(N.IsLook, S.IsLook) as IsLook," +
                "ISNULL(N.CreateTime, S.CreateTime) as CreateTime," +
                "ISNULL(N.Valid, S.Valid) as Valid " +
                "from Notice as N full join Notice_System as S  on N.CreateTime = S.CreateTime) as T " +
                "WHERE[UserID] = "+userID+
                " and[Valid] = 1 order by CreateTime desc";
               
                HF.Cloud.BLL.Common.Logger.Error("GetNotice方法联表查询语句:" + sqlString);
                dt = noticeEL.ExecuteSqlString(sqlString);
               
            }
            if (noticeType == "1")//个人通知
            {
                dt = noticeEL.ExecDT(21);
            }
            if (noticeType == "2")//系统通知
            {
                Notice_SystemEL nsEL = new Notice_SystemEL();
                nsEL.UserID = userID;
                dt = nsEL.ExecDT(21);
            }
            HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到通知个数:" + dt.Rows.Count);
            SB_UserEL userEL = new SB_UserEL();
            CompanysEL companyEL = new CompanysEL();
            GroupEL groupEL = new GroupEL();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (noticeType == "0")
                {
                    if (dr["NoticeType"].ToString() == "2")//如果是系统通知
                    {
                        dic.Add("NoticeID", dr["SID"].ToString());//联合表里系统通知表的通知ID
                        string noticeTitleStr = dr["NoticeTitle"].ToString();
                        string noticeTitle = noticeTitleStr.Length > 10 ? noticeTitleStr.Substring(0, 10) + "..." : noticeTitleStr;
                        dic.Add("NoticeTitle", noticeTitle);
                        string noticeContentStr = dr["NoticeContent"].ToString();
                        string noticeContent = noticeContentStr.Length > 20 ? noticeContentStr.Substring(0, 20) + "..." : noticeContentStr;
                        dic.Add("NoticeContent", noticeContent);
                        dic.Add("AddressUrl", dr["AddressUrl"].ToString());
                        dic.Add("NoticeType", "2");
                        dic.Add("IsLook", dr["IsLook"].ToString());
                        dic.Add("CreateTime", dr["CreateTime"].ToString());
                    }
                    else
                    {
                        long userID_Friend = long.Parse(dr["UserID_Friend"].ToString());
                        //通过UserID_Friend获取用户头像名字公司ID
                        userEL.ID = userID_Friend;
                        userEL.ExecuteEL(4);
                        HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到好友名字:" + userEL.UserName);
                        string friend_Img = userEL.ImgUrl;
                        string friend_Name = userEL.UserName;
                        string friend_Session = userEL.Session_True;
                        long friend_CompanyID = userEL.CompanyID;

                        companyEL.ID = friend_CompanyID;
                        companyEL.ExecuteEL(2);
                        HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到好友公司名字:" + companyEL.CompanyName);
                        string friend_CompanyName = companyEL.CompanyName;
                        //获取群组相关
                        if (dr["NoticeType"].ToString()=="3")//3为群组审核通知
                        {
                            long groupID = long.Parse(dr["GroupID"].ToString());
                            groupEL.ID = groupID;
                            groupEL.ExecuteEL(3);
                            HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到群组名称:" + groupEL.GroupName+"---群组ID:"+ groupID);
                            dic.Add("GroupID", groupID.ToString());//群组ID，入群审核用
                            dic.Add("GroupName",groupEL.GroupName);//群组名称，入群审核用
                        }
                       
                        dic.Add("UserName", friend_Name);
                        dic.Add("Session", friend_Session);
                        dic.Add("ImgUrl", friend_Img);
                        dic.Add("CompanyName", friend_CompanyName);
                        dic.Add("NoticeID", dr["NID"].ToString());//NID这里是用的联表查询里的个人通知的ID
                        dic.Add("CreateTime", dr["CreateTime"].ToString());
                        dic.Add("NoticeType", dr["NoticeType"].ToString());
                        dic.Add("NoticeState", dr["NoticeState"].ToString());
                        dic.Add("IsLook", dr["IsLook"].ToString());
                       
                    }
                }
                if (noticeType == "1")//个人通知
                {
                    long userID_Friend = long.Parse(dr["UserID_Friend"].ToString());
                    //通过UserID_Friend获取用户头像名字公司ID
                    userEL.ID = userID_Friend;
                    userEL.ExecuteEL(4);
                    HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到好友名字:" + userEL.UserName);
                    string friend_Img = userEL.ImgUrl;
                    string friend_Name = userEL.UserName;
                    string friend_Session = userEL.Session_True;
                    long friend_CompanyID = userEL.CompanyID;

                    companyEL.ID = friend_CompanyID;
                    companyEL.ExecuteEL(2);
                    HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到好友公司名字:" + companyEL.CompanyName);
                    string friend_CompanyName = companyEL.CompanyName;
                    //获取群组相关
                    if (dr["NoticeType"].ToString() == "3")//3为群组审核通知
                    {
                        long groupID = long.Parse(dr["GroupID"].ToString());
                        groupEL.ID = groupID;
                        groupEL.ExecuteEL(3);
                        HF.Cloud.BLL.Common.Logger.Error("GetNotice方法获取到群组名称:" + groupEL.GroupName + "---群组ID:" + groupID);
                        dic.Add("GroupID", groupID.ToString());//群组ID，入群审核用
                        dic.Add("GroupName", groupEL.GroupName);//群组名称，入群审核用
                    }
                    dic.Add("UserName", friend_Name);
                    dic.Add("Session", friend_Session);
                    dic.Add("ImgUrl", friend_Img);
                    dic.Add("CompanyName", friend_CompanyName);
                    dic.Add("NoticeID", dr["ID"].ToString());//ID:这里是单个Notice表里的ID
                    dic.Add("CreateTime", dr["CreateTime"].ToString());
                    dic.Add("NoticeType", dr["NoticeType"].ToString());
                    dic.Add("NoticeState", dr["NoticeState"].ToString());
                    dic.Add("IsLook", dr["IsLook"].ToString());
                }
                if (noticeType == "2")//系统通知
                {
                    dic.Add("NoticeID", dr["ID"].ToString());//系统通知表的通知ID
                    string noticeTitleStr = dr["NoticeTitle"].ToString();
                    string noticeTitle = noticeTitleStr.Length > 10 ? noticeTitleStr.Substring(0, 10) + "..." : noticeTitleStr;
                    dic.Add("NoticeTitle", noticeTitle);
                    string noticeContentStr = dr["NoticeContent"].ToString();
                    string noticeContent = noticeContentStr.Length > 20 ? noticeContentStr.Substring(0, 20) + "..." : noticeContentStr;
                    dic.Add("NoticeContent", noticeContent);
                    dic.Add("AddressUrl", dr["AddressUrl"].ToString());
                    dic.Add("NoticeType", "2");
                    dic.Add("IsLook", dr["IsLook"].ToString());
                    dic.Add("CreateTime", dr["CreateTime"].ToString());
                }
                
                list.Add(dic);
            }
            ////调用此接口后默认都查看通知了，把通知都变为已经查看
            //noticeEL.IsLook = 1;
            //int ra;
            //long noticQue = noticeEL.ExecNonQuery(3, out ra);
            //HF.Cloud.BLL.Common.Logger.Error("GetNotice方法更改为已经查看的记录数为:" + ra.ToString());
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("GetNotice方法返回json数据：" + strJson);
            return strJson;
        }


        /// <summary>
        /// 获取未查看通知个数
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        public string NoLookNoticeNumber(string session)
        {
            HF.Cloud.BLL.Common.Logger.Error("NoLookNoticeNumber方法获取到参数session:" + session);
            //通过session获取用户ID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            HF.Cloud.BLL.Common.Logger.Error("NoLookNoticeNumber方法获取到userID:" + userID);
            NoticeEL noticeEL = new NoticeEL();
            noticeEL.UserID = userID;
            //个人通知+公司通知+系统通知
            string sqlString = "select* from (" +
                "select N.ID as NID, S.ID as SID, N.UserID_Friend,N.GroupID," +
                "ISNULL(N.UserID, S.UserID) as UserID," +
                "ISNULL(N.NoticeType, S.NoticeType) as NoticeType," +
                "N.NoticeState, S.NoticeTitle, S.NoticeContent, s.AddressUrl," +
                "ISNULL(N.IsLook, S.IsLook) as IsLook," +
                "ISNULL(N.CreateTime, S.CreateTime) as CreateTime," +
                "ISNULL(N.Valid, S.Valid) as Valid " +
                "from Notice as N full join Notice_System as S  on N.CreateTime = S.CreateTime) as T " +
                "WHERE[UserID] = " + userID +
                " and[Valid] = 1 and IsLook=0 order by CreateTime desc";
            DataTable dt = noticeEL.ExecuteSqlString(sqlString);
            HF.Cloud.BLL.Common.Logger.Error("NoLookNoticeNumber方法获取到未读通知个数:" + dt.Rows.Count);
            return dt.Rows.Count.ToString();
        }



        /// <summary>
        /// 通知忽略
        /// </summary>
        /// <param name="noticeID">通知ID</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知</param>
        /// <returns></returns>
        public string NoticeHuLue(string session,string noticeID,string noticeType)
        {
            string resultStr = "";
            HF.Cloud.BLL.Common.Logger.Error("NoticeHuLue方法获取参数session："+ session + "noticeID:" + noticeID);
            NoticeEL noticeEL = new NoticeEL();
            noticeEL.ID = long.Parse(noticeID);
            noticeEL.NoticeState = 2;
            int ra;
            noticeEL.ExecNonQuery(31, out ra);
            HF.Cloud.BLL.Common.Logger.Error("NoticeHuLue方法更改NoticeState受影响的行数:" + ra);
            //返回通知列表
            resultStr = GetNotice(session,noticeType);
            return resultStr;
        }


        /// <summary>
        /// 通知列表中同意添加好友
        /// </summary>
        /// <param name="session">本人的session</param>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="sessionFriend">好友的session</param>
        /// <param name="noticeType">标志要请求的通知类型，0全部通知，1个人通知，2系统通知,</param>
        /// <returns></returns>
        public string AddFriendByNotice(string session,string noticeID,string sessionFriend,string noticeType)
        {
            HF.Cloud.BLL.Common.Logger.Error("AddFriendByNotice方法获取到的参数session:" + session+ "---sessionFriend:" + sessionFriend+ "---noticeType:"+ noticeType);
            NoticeEL noticeEL = new NoticeEL();
            //通过noticeID获取NoticeType=3？说明是审核的通知，修改UserUniteGroup表valid=1
            noticeEL.ID = long.Parse(noticeID);
            DataTable dtNotice = noticeEL.ExecDT(2);
            if(dtNotice!=null&dtNotice.Rows.Count>0)
            {
                //如果是加群审核通知
                if(dtNotice.Rows[0]["NoticeType"].ToString()=="3")
                {
                    long groupID = (long)dtNotice.Rows[0]["GroupID"];
                    //UserUniteGroup表中的valid改为1，则通过审核
                    UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
                    userGroupEL.UserID = long.Parse(dtNotice.Rows[0]["UserID_Friend"].ToString());
                    userGroupEL.GroupID = groupID;
                    int raG;
                    userGroupEL.ExecNonQuery(21, out raG);
                    //通知notice表中的状态NoticeState要给成1通过
                    NoticeEL notice_EL = new NoticeEL();
                    notice_EL.ID = long.Parse(noticeID);
                    notice_EL.NoticeState = 1;
                    notice_EL.IsLook = 1;
                    int raNoti;
                    notice_EL.ExecNonQuery(33,out raNoti);
                    HF.Cloud.BLL.Common.Logger.Error("AddFriendByNotice方法更改UserUniteGroupEL的Valid受影响的行数:" + raG+"更改通知表结果："+ raNoti);
                    //审核后给用户发通知（系统通知）
                    GroupEL groupEL = new GroupEL();
                    groupEL.ID = groupID;
                    DataTable dt_group = groupEL.ExecDT(3);
                    if(dt_group!=null&&dt_group.Rows.Count>0)
                    {
                        string groupName = dt_group.Rows[0]["GroupName"].ToString();
                        Notice_SystemEL noticeSystemEL = new Notice_SystemEL();
                        noticeSystemEL.UserID = long.Parse(dtNotice.Rows[0]["UserID_Friend"].ToString());
                        noticeSystemEL.NoticeTitle = "审核通知！";
                        noticeSystemEL.NoticeContent = "您加入群\"" + groupName + "\"的申请已通过！";
                        noticeSystemEL.AddressUrl = "groupid="+ groupID;
                        noticeSystemEL.NoticeType = 2;
                        noticeSystemEL.IsLook = 0;
                        noticeSystemEL.CreateTime = DateTime.Now.ToString();
                        noticeSystemEL.Valid = 1;
                        int raNS;
                        noticeSystemEL.ExecNonQuery(1, out raNS);
                    }
                }
                else
                {
                    //添加好友
                    FriendsBLL friendsBLL = new FriendsBLL();
                    string addFriendResult = friendsBLL.AddFriend(session, sessionFriend);
                    //修改通知表中记录的状态改为好友
                    noticeEL.ID = long.Parse(noticeID);
                    noticeEL.NoticeState = 1;
                    int ra;
                    noticeEL.ExecNonQuery(31, out ra);
                    HF.Cloud.BLL.Common.Logger.Error("AddFriendByNotice方法更改NoticeState受影响的行数:" + ra);
                }
            }
            
            //返回通知列表
            string resultStr = GetNotice(session,noticeType);
            return resultStr;
        }


        /// <summary>
        /// 更改通知状态为已经查看
        /// </summary>
        /// <param name="noticeID">通知的ID</param>
        /// <param name="noticeType">通知的类型0个人，1公司，2系统</param>
        /// <returns></returns>
        public string UpdateNoticeLooked(string noticeID,string noticeType)
        {
            string returnStr = "";
            HF.Cloud.BLL.Common.Logger.Error("UpdateNoticeLooked方法更改通知为已经查看接受的参数noticeID:" + noticeID + "---noticeType:" + noticeType);
            if(noticeType=="2")//系统通知
            {
                Notice_SystemEL noticeEL = new Notice_SystemEL();
                noticeEL.ID = long.Parse(noticeID);
                noticeEL.IsLook = 1;
                int ra;
                long returnValue=noticeEL.ExecNonQuery(3, out ra);
                HF.Cloud.BLL.Common.Logger.Error("UpdateNoticeLooked方法修改系统通知为已经查看状态结果:" + ra);
                if (ra>0)
                {
                    returnStr = "success";
                }
                else
                {
                    returnStr = "error";
                }
            }
            else
            {
                NoticeEL noticeEL = new NoticeEL();
                noticeEL.ID = long.Parse(noticeID);
                noticeEL.IsLook = 1;
                int ra;
                long returnValue = noticeEL.ExecNonQuery(3, out ra);
                HF.Cloud.BLL.Common.Logger.Error("UpdateNoticeLooked方法修改普通通知为已经查看状态结果:" + ra);
                if (ra > 0)
                {
                    returnStr = "success";
                }
                else
                {
                    returnStr = "error";
                }

            }
            return returnStr;
        }


        /// <summary>
        /// 获取图片路径列表
        /// </summary>
        /// <returns></returns>
        public string GetSystemPicture()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            NoticeEL noticeEL = new NoticeEL();
            DataTable dt= noticeEL.ExecuteSqlString("select * from T_Picture where Valid=1 order by Sort");
            foreach(DataRow dr in dt.Rows)
            {
                string httpStr = "https://shangwulink.com/Images/SWLPicture/";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                string picName = httpStr + dr["PictureName"].ToString();
                dic.Add("PictureName", picName);
                list.Add(dic);
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("GetSystemPicture方法返回的json数据：" + strJson);
            return strJson;
            
        }





    }
}
