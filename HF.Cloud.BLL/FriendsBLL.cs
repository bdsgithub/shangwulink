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
    public   class FriendsBLL
    {

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">好友session</param>
        /// <returns></returns>
        public string AddFriend(string session,string session_Friend)
        {
            HF.Cloud.BLL.Common.Logger.Error("AddFriend方法接受的参数session：" + session + "---session_Friend:" + session_Friend);
            string resultStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            long userID_Friend = userBLL.GetUserIDBySession(session_Friend);


            FriendsEL frel = new FriendsEL();
            frel.UserID = userID;
            frel.UserID_Friend = userID_Friend;
            //查看userid是否加过userid_friend好友
            frel.ExecuteEL(3);
            HF.Cloud.BLL.Common.Logger.Error("AddFriend方法是否加过好友（大于0说明曾经加过好友）：" + frel.ID);
            if (frel.ID>0)
            {
                //曾经加过或本就是好友，直接修改valid=1即可
                int outValue;
                long executeResul =frel.ExecNonQuery(2, out outValue);
                if(executeResul>0)
                {
                    resultStr = "success";
                }
                else
                {
                    resultStr = "error";
                }
            }
            else
            {
                //添加一条好友记录 
                frel.CreateTime = DateTime.Now.ToString();
                frel.Valid = 1;
                int outValue;
                long executeResul = frel.ExecNonQuery(1, out outValue);
                HF.Cloud.BLL.Common.Logger.Error("AddFriend方法添加好友结果（大于0说明添加成功）：" + executeResul);
                if (executeResul>0)
                {
                    //查看对方加过我没有
                    FriendsEL frel_T = new FriendsEL();
                    frel_T.UserID = userID_Friend;
                    frel_T.UserID_Friend = userID;
                    frel_T.ExecuteEL(3);
                    //如果没加过就给userID_Friend发通知
                    if (frel_T.ID <= 0)
                    {
                        //在通知表中给userID_Friend添加一个通知记录
                         NoticeEL noticeEL = new NoticeEL();

                        noticeEL.UserID = userID_Friend;//接受通知用户的userID
                        noticeEL.UserID_Friend = userID;//
                        noticeEL.NoticeType = 0;
                        noticeEL.NoticeState = 0;
                        noticeEL.IsLook = 0;
                        noticeEL.CreateTime = DateTime.Now.ToString();
                        noticeEL.Valid = 1;
                        int ra;
                        long notiExec = noticeEL.ExecNonQuery(1, out ra);
                        HF.Cloud.BLL.Common.Logger.Error("AddFriend方法添加对方的通知结果（大于0说明添加成功）：" + notiExec);
                        resultStr = "success";
                    }
                }
                else
                {
                    resultStr = "error";
                }

            }
            return resultStr;
        }




        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="session">本人session</param>
        /// <returns></returns>
        public string GetFriendList(string session) 
        {
            HF.Cloud.BLL.Common.Logger.Error("FriendList获取到的session为" + session);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //session获取用户UserID
            SB_UserEL userEL = new SB_UserEL();
            userEL.Session_True = session;
            userEL.ExecuteEL(41);
            HF.Cloud.BLL.Common.Logger.Error("userEL.ID：" + userEL.ID);
            if (userEL.ID>0)
            {
                long userID = userEL.ID;
                //通过userID在好友表中查询好友列表
                FriendsEL frenEL = new FriendsEL();
                frenEL.UserID = userID;
                DataTable dt = frenEL.ExecDT(31);
                HF.Cloud.BLL.Common.Logger.Error("获取到好友列表，好友个数为：" + dt.Rows.Count);
                
                foreach (DataRow dr in dt.Rows)
                {
                    long userid_Friend = long.Parse(dr["UserID_Friend"].ToString());
                    //通过userid_Friend获取好友信息
                    userEL.ID = userid_Friend;
                    userEL.ExecuteEL(4);
                    string userName = userEL.UserName;
                    string userTel = userEL.UserTel;
                    string duty = userEL.Duty;
                    string imgUrl = userEL.ImgUrl; 
                    string session_Friend = userEL.Session_True;
                    long companyID = userEL.CompanyID;
                    //通过companyID获取公司名称
                    CompanysEL compaEL = new CompanysEL();
                    compaEL.ID = companyID;
                    compaEL.ExecuteEL(2);
                    string companyName = compaEL.CompanyName;
                    HF.Cloud.BLL.Common.Logger.Error("好友名称：" + userEL.UserName+"公司名称："+ companyName);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("UserName", userName);
                    dic.Add("UserTel", userTel);
                    dic.Add("Duty", duty);
                    dic.Add("ImgUrl", imgUrl); 
                    dic.Add("Session", session_Friend);
                    dic.Add("CompanyName", companyName);

                    list.Add(dic);
                }
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("返回的好友列表json数据：" + strJson);
            return strJson;
        }


        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="session_Friend">要删除的好友session</param>
        /// <returns></returns>
        public string CancelFriend(string session,string session_Friend)
        {
            HF.Cloud.BLL.Common.Logger.Error("CancelFriend获取到的session为" + session+ "------session_Friend：" + session_Friend);
            string resultStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            long userID_Friend = userBLL.GetUserIDBySession(session_Friend);
            HF.Cloud.BLL.Common.Logger.Error("CancelFriend获取到的userID为" + userID + "------userID_Friend：" + userID_Friend);
            FriendsEL frel = new FriendsEL();
            frel.UserID = userID;
            frel.UserID_Friend = userID_Friend;
            int outValue;
            long queryResult= frel.ExecNonQuery(21, out outValue);
            HF.Cloud.BLL.Common.Logger.Error("执行删除结果为（大于0删除成功）:" + queryResult);
            if (outValue > 0)
            {
                resultStr = "success";
            }
            else
            {
                resultStr = "error";
            }
            
            return resultStr;
        }



        /// <summary>
        /// 判断sessionFriend是否是session的好友
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="sessionFriend">（好友）session</param>
        /// <returns></returns>
        public bool IsFriend(string session,string sessionFriend)
        {
            bool isFriend = false;
            //通过session获取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            long userIDFriend = userBLL.GetUserIDBySession(sessionFriend);
            FriendsEL friendEL = new FriendsEL();
            friendEL.UserID = userID;
            friendEL.UserID_Friend = userIDFriend;
            DataTable dt= friendEL.ExecDT(32);
            HF.Cloud.BLL.Common.Logger.Error("IsFriend方法执行查找好友结果:" + dt.Rows.Count+"---userID:"+userID+"---userIDFriend:"+ userIDFriend);
            if (dt.Rows.Count> 0)
            {
                isFriend = true;
            }
            return isFriend;
        }



        /// <summary>
        /// 查看是多少人的好友，被多少人添加好友，保存数
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public int GetSaveNumber(string session)
        {
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            FriendsEL friendsEL = new FriendsEL();
            friendsEL.UserID_Friend = userID;
            DataTable dt = friendsEL.ExecDT(33);
            int sum = dt.Rows.Count;
            return sum;
        }


        /// <summary>
        /// 搜索好友
        /// </summary>
        /// <param name="friendName">好友名称</param>
        /// <returns></returns>
        public string SearchFriend(string session,string friendName)
        {
            HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法获取的参数session:"+ session + "---friendName:" + friendName);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);

            FriendsEL friendEL = new FriendsEL();
            DataTable dt = friendEL.ExecuteSqlString("select * from ViewFriends where UserID=" + userID + " and UserName like '%" + friendName + "%' and Friend_Valid=1");
                
            SB_UserEL userEL = new SB_UserEL();
            //userEL.UserName = friendName;
            //DataTable dt= userEL.ExecDT(45);
            HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法获取的好友个数为:" + dt.Rows.Count);
            if(dt!=null&&dt.Rows.Count>0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //这里都是用的视图ViewFriends里的字段
                    long userid = long.Parse(dr["SB_User_ID"].ToString());
                    //通过userid_Friend获取好友信息
                    userEL.ID = userid;
                    userEL.ExecuteEL(4);
                    string userName = userEL.UserName;
                    string userTel = userEL.UserTel;
                    string duty = userEL.Duty;
                    string imgUrl = userEL.ImgUrl;
                    string session_Friend = userEL.Session_True;
                    long companyID = userEL.CompanyID;
                    //通过companyID获取公司名称
                    CompanysEL compaEL = new CompanysEL();
                    compaEL.ID = companyID;
                    compaEL.ExecuteEL(2);
                    string companyName = compaEL.CompanyName;
                    HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法好友名称：" + userEL.UserName + "公司名称：" + companyName);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("UserName", userName);
                    dic.Add("UserTel", userTel);
                    dic.Add("Duty", duty);
                    dic.Add("ImgUrl", imgUrl);
                    dic.Add("Session", session_Friend);
                    dic.Add("CompanyName", companyName);

                    list.Add(dic);
                }
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法返回的好友列表json数据：" + strJson);
            return strJson;
        }






    }
}
