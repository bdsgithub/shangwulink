using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


using HF.Cloud.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;

namespace HF.Cloud.BLL
{
  public  class GroupBLL
    {
        /// <summary>
        /// 新建群组  修改群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="Introduce">群组简介</param>
        /// <param name="isOpen">公开或私密</param>
        /// <param name="groupID">群组ID 0添加 大于0修改</param>
        /// <returns></returns>
        public string CreateGroup(string session,string groupName,string introduce,string isOpen,string groupID)
        {
            string result = "";
            HF.Cloud.BLL.Common.Logger.Error("CreateGroup方法获取到的参数session:" + session + "----groupName:" + groupName + "---Introduce:" + introduce + "---isOpen:" + isOpen + "---groupID:" + groupID);
            GroupEL groupEL = new GroupEL();
            groupEL.GroupName = groupName;
            groupEL.Introduce = introduce;
            groupEL.CreateTime = DateTime.Now.ToString();
            //通过session获取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            groupEL.OwnerUserID = userID;
            groupEL.IsOpen = Int32.Parse(isOpen);
            groupEL.Valid = 1;
            long groupResultID=0;//添加群组返回值
            long updateGroupResultID = 0;//修改群组返回值
            if (groupID == "0")//添加群组
            {
                int ra;
                groupResultID = groupEL.ExecNonQuery(1, out ra);
                HF.Cloud.BLL.Common.Logger.Error("CreateGroup方法创建群组后结果（大于0成功）:" + groupResultID);
            }
            else//修改群组
            {
                groupEL.ID = long.Parse(groupID);
                int ra;
                updateGroupResultID=groupEL.ExecNonQuery(22, out ra);
                //updateGroupResultID = groupID;
                HF.Cloud.BLL.Common.Logger.Error("CreateGroup方法修改群组后结果（大于0成功）:" + ra);
            }
          
            if (groupResultID>0)
            {
                //创建完群组需要把用户在用户与群关联表中添加记录关联起来
                UserUniteGroupEL uugEL = new UserUniteGroupEL();
                uugEL.UserID = userID;
                uugEL.GroupID = groupResultID;
                uugEL.CreateTime = DateTime.Now.ToString();
                uugEL.IsTop = 0;
                uugEL.Valid = 1;
                int uugRa;
                long uugResultID = uugEL.ExecNonQuery(1, out uugRa);
                HF.Cloud.BLL.Common.Logger.Error("CreateGroup方法创建群组后用户与群组关联结果（大于0成功）:" + uugResultID);
                if (uugResultID>0)
                {
                    result = groupResultID.ToString();
                }
                else
                {
                    result = "error";
                }
            }
            else if(updateGroupResultID>0)
            {
                result = groupID;
            }
            else
            {
                result= "error";
            }
            return result;
        }


        /// <summary>
        /// 我的群组列表
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        public string GetMyGroups(string session)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到的参数session:" + session);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //通过Session获取UerID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到的UserID:" + userID);
            //通过userID获取GroupID
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            userGroupEL.UserID = userID;
            DataTable dt = userGroupEL.ExecDT(3);
            HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到群组个数为:" + dt.Rows.Count);
            GroupEL groupEL = new GroupEL();
            SB_UserEL userEL = new SB_UserEL();
            foreach (DataRow dr in dt.Rows)
            {
                //通过群ID获取群信息
                long groupID = (long)dr["GroupID"];
                groupEL.ID = groupID;
                groupEL.ExecuteEL(3);
                HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到群组名称:" + groupEL.GroupName);
                string groupName = "";
                long ownerUserID = 0;
                string createTime = "";
                if (!string.IsNullOrEmpty(groupEL.GroupName))
                {
                    groupName = groupEL.GroupName;
                    ownerUserID = groupEL.OwnerUserID;
                    createTime = groupEL.CreateTime;
                }
                //通过ownerUserID获取群主Name
                userEL.ID = ownerUserID;
                userEL.ExecuteEL(4);
                HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到群主姓名:" + userEL.UserName);
                string ownerUserName = "";
                if (!string.IsNullOrEmpty(userEL.UserName))
                {
                    ownerUserName = userEL.UserName;
                }
                //通过群ID获取群有多少人
                userGroupEL.GroupID = groupID;
                DataTable dt_Count= userGroupEL.ExecDT(31);
                HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法获取到本群人数:" + dt_Count.Rows.Count);
                int groupCount = dt_Count.Rows.Count;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("GroupName", groupName);
                dic.Add("GroupOwnerName", ownerUserName);
                dic.Add("CreateTime", createTime);
                dic.Add("GroupCount", groupCount.ToString());
                dic.Add("GroupID", groupID.ToString());
                list.Add(dic);
            }
            
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("GetMyGroups方法返回的json数据：" + strJson);
            return strJson;
        }



        /// <summary>
        /// 群组人员列表
        /// </summary>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string GetGroupFriendList(string session,string groupID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetGroupFriendList方法获取到的参数groupID:" + groupID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //通过groupID获取群信息
            GroupEL groupEL = new GroupEL();
            SB_UserEL userEL = new SB_UserEL();
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            long groupID_long =long.Parse(groupID);
            groupEL.ID = groupID_long;
            groupEL.ExecuteEL(3);
            HF.Cloud.BLL.Common.Logger.Error("GetGroupFriendList方法获取到群组名称:" + groupEL.GroupName);
            string groupName = "";  //群名称
            long ownerUserID = 0;  //群主userID
            string introduce = "";  //群简介
            string isOpen = "";//1公开或0私密
            if (!string.IsNullOrEmpty(groupEL.GroupName))
            {
                groupName = groupEL.GroupName;
                ownerUserID = groupEL.OwnerUserID;
                introduce = groupEL.Introduce;
                isOpen = groupEL.IsOpen.ToString();
            }
            //通过ownerUserID获取群主Name
            userEL.ID = ownerUserID;
            userEL.ExecuteEL(4);
            HF.Cloud.BLL.Common.Logger.Error("GetGroupFriendList方法获取到群主姓名:" + userEL.UserName);
            string ownerUserName = "";
            if (!string.IsNullOrEmpty(userEL.UserName))
            {
                ownerUserName = userEL.UserName;
            }
            string userSession = userEL.Session_True;
            //通过群ID获取群有多少人
            userGroupEL.GroupID = groupID_long;
            DataTable dt_Count = userGroupEL.ExecDT(31);
            HF.Cloud.BLL.Common.Logger.Error("GetGroupFriendList方法获取到本群人数:" + dt_Count.Rows.Count);
            int groupCount = dt_Count.Rows.Count;
            foreach(DataRow dr in dt_Count.Rows)
            {
                //获取到userID
                string userID = dr["UserID"].ToString();
                UserBLL userBLL = new UserBLL();
                string userInfo = userBLL.GetUserInfoByUserID(userID);
                //把userInfo反序列化出来
                Dictionary<string, object> json_userInfo = new Dictionary<string, object>();
                json_userInfo = js.Deserialize<Dictionary<string, object>>(userInfo);
                //加上置顶字段 IsTop 2018-1-15
                json_userInfo.Add("IsTop", dr["IsTop"].ToString());
                list.Add(json_userInfo);
            }
            dic.Add("GroupName", groupName);
            dic.Add("GroupOwnerName", ownerUserName);
            dic.Add("Introduce", introduce);
            dic.Add("IsOpen", isOpen);
            dic.Add("GroupCount", groupCount.ToString());
            dic.Add("GroupID", groupID.ToString());
            dic.Add("IsGroupOwner", session==userSession?"1":"0");
            dic.Add("User", list);

            string strJson = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetGroupFriendList方法返回的json数据：" + strJson);
            return strJson;
        }






        /// <summary>
        /// 面对面加群(创建新群或加群)
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="password">口令</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public string FaceToFaceCreateGroup(string session,string password,string lon,string lat)
        {
            string result = "";
            HF.Cloud.BLL.Common.Logger.Error("FaceToFaceCreateGroup方法获取的参数session：" + session+ "----password:" + password+ "---lon:" + lon+"---lat："+lat);
            if (!string.IsNullOrEmpty(session) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(lon) && !string.IsNullOrEmpty(lat))
            {
                //通过session获取UserID
                UserBLL userBLL = new UserBLL();
                long userID = userBLL.GetUserIDBySession(session);//用户ID
                //先查找群组Group表中查找当前时间段内是否有口令password的数据
                GroupEL groupEL = new GroupEL();
                groupEL.GroupPassword = password;
                DataTable dt = groupEL.ExecDT(31);
                HF.Cloud.BLL.Common.Logger.Error("FaceToFaceCreateGroup方法获取的群组个数为：" + dt.Rows.Count);
                if (dt.Rows.Count > 0)//大于0说明可能已经有人创建了此群，继续验证经纬度
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        float lon_dt = float.Parse(dt.Rows[i]["lon"].ToString());
                        float lat_dt = float.Parse(dt.Rows[i]["lat"].ToString());
                        float lon_u = float.Parse(lon);
                        float lat_u = float.Parse(lat);
                        float lon_re = System.Math.Abs(lon_u - lon_dt);
                        float lat_re = System.Math.Abs(lat_u - lat_dt);
                        //经纬度111.3222222m对应0.001度
                        //如果距离够小的话说明已经有面对面群，直接加入关联群即可
                        if (lon_re < 0.1 && lat_re < 0.1)
                        {
                            UserUniteGroupEL uugEL = new UserUniteGroupEL();
                            //判断是否已经在群里面，如果在群里面则跳出循环，结束
                            uugEL.UserID = userID;
                            uugEL.GroupID = (long)dt.Rows[i]["ID"];
                            DataTable dt_IsInGroup = uugEL.ExecDT(33);
                            if (dt_IsInGroup != null && dt_IsInGroup.Rows.Count > 0)
                            {
                                result = dt.Rows[i]["ID"].ToString();//返回群组ID
                            }
                            else
                            {
                                //把用户在用户与群关联表中添加记录关联起来
                                uugEL.UserID = userID;
                                uugEL.GroupID = (long)dt.Rows[i]["ID"];
                                uugEL.CreateTime = DateTime.Now.ToString();
                                uugEL.IsTop = 0;
                                uugEL.Valid = 1;
                                int uugRa;
                                long uugResultID = uugEL.ExecNonQuery(1, out uugRa);
                                HF.Cloud.BLL.Common.Logger.Error("FaceToFaceCreateGroup方法用户与群组关联结果（大于0成功）:" + uugResultID);
                                if (uugResultID > 0)
                                {
                                    result = dt.Rows[i]["ID"].ToString();//返回群组ID
                                }
                                else
                                {
                                    result = "error";
                                }
                            }
                            break;//跳出for循环
                        }
                        else if (i == dt.Rows.Count - 1)//执行到最后依然没找到，那么就新创建个面对面群
                        {
                            result = CreateNewGroup(userID.ToString(), password, lon, lat);
                        }

                        else  //距离太大，不是当前的面对面群，继续找其他符合条件的群
                        {
                            continue;
                        }
                    }
                }
                else//无记录说明自己是第一个，然后创建新的面对面群
                {
                    result = CreateNewGroup(userID.ToString(), password, lon, lat);
                }
            }
            else
            {
                result = "error";
            }
            HF.Cloud.BLL.Common.Logger.Error("FaceToFaceCreateGroup方法返回的数据为：" + result);
            return result;
        }



        /// <summary>
        /// 创建新的面对面群
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">口令</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns></returns>
        public string CreateNewGroup(string userID, string password, string lon, string lat)
        {
           string result = "";
            GroupEL groupEL = new GroupEL();
            groupEL.GroupPassword = password;
            groupEL.GroupName = "面对面群组" + DateTime.Now.ToString("hhmmss");
            groupEL.Introduce = "面对面群组";
            groupEL.CreateTime = DateTime.Now.ToString();
            groupEL.Lon = lon;
            groupEL.Lat = lat;
            
            groupEL.OwnerUserID = long.Parse(userID);
            groupEL.Valid = 1;
            int ra;
            long groupResultID = groupEL.ExecNonQuery(1, out ra);
            HF.Cloud.BLL.Common.Logger.Error("CreateNewGroup方法创建群组后结果（大于0成功）:" + groupResultID);

            if (groupResultID > 0)
            {
                //创建完群组需要把用户在用户与群关联表中添加记录关联起来
                UserUniteGroupEL uugEL = new UserUniteGroupEL();
                uugEL.UserID = long.Parse(userID);
                uugEL.GroupID = groupResultID;
                uugEL.CreateTime = DateTime.Now.ToString();
                uugEL.IsTop = 0;
                uugEL.Valid = 1;
                int uugRa;
                long uugResultID = uugEL.ExecNonQuery(1, out uugRa);
                HF.Cloud.BLL.Common.Logger.Error("CreateNewGroup方法创建群组后用户与群组关联结果（大于0成功）:" + uugResultID);
                if (uugResultID > 0)
                {
                    result = groupResultID.ToString();
                }
                else
                {
                    result = "error";
                }
            }
            else
            {
                result = "error";
            }
            return result;
        }



        /// <summary>
        /// 搜索群
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="groupName">群名称</param>
        /// <returns></returns>
        public string  SearchGroup(string session,string groupName)
        {
            HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法获取的参数session:" + session + "---groupName:" + groupName);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //通过Session获取UerID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            HF.Cloud.BLL.Common.Logger.Error("SearchFriend方法获取到的UserID:" + userID);
            GroupEL groupEL = new GroupEL();
            //获取到userID用户所有群中包含groupName名字的群组
            DataTable dt = groupEL.ExecuteSqlString("select * from ViewGroupUnite where UserID=" + userID + " and GroupName like '%" + groupName + "%' and Group_Valid=1 and Unite_Valid=1");
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            SB_UserEL userEL = new SB_UserEL();
            if(dt!=null&&dt.Rows.Count>0)
            { 
                foreach (DataRow dr in dt.Rows)
                 {
                        ////通过群ID获取群信息
                        //long groupID = (long)/*dr["GroupID"]*/;
                        //groupEL.ID = groupID;
                        //groupEL.ExecuteEL(3);
                        //HF.Cloud.BLL.Common.Logger.Error("SearchGroup方法获取到群组名称:" + groupEL.GroupName);
                     string groupName_true = dr["GroupName"].ToString();
                    long ownerUserID = (long)dr["OwnerUserID"];
                    string createTime = dr["Group_CreateTime"].ToString(); 
                    //if (!string.IsNullOrEmpty(groupEL.GroupName))
                    //{
                    //    groupName_true = groupEL.GroupName;
                    //    ownerUserID = groupEL.OwnerUserID;
                    //    createTime = groupEL.CreateTime;
                    //}
                    //通过ownerUserID获取群主Name
                    userEL.ID = ownerUserID;
                    userEL.ExecuteEL(4);
                    HF.Cloud.BLL.Common.Logger.Error("SearchGroup方法获取到群主姓名:" + userEL.UserName);
                    string ownerUserName = "";
                    if (!string.IsNullOrEmpty(userEL.UserName))
                    {
                        ownerUserName = userEL.UserName;
                    }
                    //通过群ID获取群有多少人
                    userGroupEL.GroupID = (long)dr["Group_ID"];
                    DataTable dt_Count = userGroupEL.ExecDT(31);
                    HF.Cloud.BLL.Common.Logger.Error("SearchGroup方法获取到本群人数:" + dt_Count.Rows.Count);
                    int groupCount = dt_Count.Rows.Count;

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("GroupName", groupName_true);
                    dic.Add("GroupOwnerName", ownerUserName);
                    dic.Add("CreateTime", createTime);
                    dic.Add("GroupCount", groupCount.ToString());
                    dic.Add("GroupID", dr["Group_ID"].ToString());
                    list.Add(dic);
                }
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("SearchGroup方法返回的json数据：" + strJson);
            return strJson;
            
        }

        

        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string  IntoGroup(string session,string groupID)
        {
            string result = "";
            HF.Cloud.BLL.Common.Logger.Error("IntoGroup方法获取的参数session：" + session + "----groupID:" + groupID);
            //通过session获取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);//用户ID
            //判断群是否是私密的群
            GroupEL groupEL = new GroupEL();
            groupEL.ID = long.Parse(groupID);
            DataTable dtGroup = groupEL.ExecDT(3);
           
            if (dtGroup!=null&&dtGroup.Rows.Count>0)
            {
                //先查看数据库中是否有记录
                UserUniteGroupEL userUniteGroupEL = new UserUniteGroupEL();
                userUniteGroupEL.UserID = userID;
                userUniteGroupEL.GroupID = long.Parse(groupID);
                DataTable dt = userUniteGroupEL.ExecDT(32);
                //如果有记录，就不添加直接修改valid就可以
                if(dt != null && dt.Rows.Count > 0)
                {
                    //如果是私密群还需修改通知
                    if (dtGroup.Rows[0]["IsOpen"].ToString() == "0")
                    {
                        NoticeEL noticeEL = new NoticeEL();
                        noticeEL.UserID_Friend = userID;
                        noticeEL.GroupID = long.Parse(groupID);
                        noticeEL.NoticeState = 0;
                        noticeEL.IsLook = 0;
                        noticeEL.CreateTime = DateTime.Now.ToString();
                        noticeEL.Valid = 1;
                        int raNotice;
                        noticeEL.ExecNonQuery(32, out raNotice);
                        HF.Cloud.BLL.Common.Logger.Error("IntoGroup方法从新加入群组，跟新通知，修改结果（大于0成功）：" + raNotice);
                        if(raNotice > 0)
                        {
                            result = "secsuccess";
                        }
                        else
                        {
                            result = "error";
                        }
                    }
                    else
                    {
                        //修改用户和群关联表
                        int raup;
                        userUniteGroupEL.ExecNonQuery(21, out raup);
                        HF.Cloud.BLL.Common.Logger.Error("IntoGroup方法用户已经在群组，修改Valid结果（大于0成功）：" + raup);
                        if (raup > 0)
                        {
                            result = "success";
                        }
                        else
                        {
                            result = "error";
                        }
                    }
                 
                }
                //无记录就添加（包括添加通知）
                else
                {
                    int userUniteGroup_Valid = 1;
                    //0是私密的群需要审核(发通知),修改Valid为2
                    if (dtGroup.Rows[0]["IsOpen"].ToString() == "0")
                    {
                        userUniteGroup_Valid = 2;//暂时无效，需要审核
                        //发通知
                        NoticeEL noticeEL = new NoticeEL();
                        noticeEL.UserID = (long)dtGroup.Rows[0]["OwnerUserID"];
                        noticeEL.UserID_Friend = userID;
                        noticeEL.GroupID = long.Parse(groupID);
                        noticeEL.NoticeType = 3;//3是加入群审核
                        noticeEL.NoticeState = 0;
                        noticeEL.IsLook = 0;
                        noticeEL.CreateTime = DateTime.Now.ToString();
                        noticeEL.Valid = 1;
                        int raN;
                        long returnVelue = noticeEL.ExecNonQuery(1, out raN);
                        HF.Cloud.BLL.Common.Logger.Error("IntoGroup方法发审核通知，（大于0成功）：" + returnVelue);
                    }
                    userUniteGroupEL.CreateTime = DateTime.Now.ToString();
                    userUniteGroupEL.IsTop = 0;
                    userUniteGroupEL.Valid = userUniteGroup_Valid;
                    int ra;
                    long exeResult = userUniteGroupEL.ExecNonQuery(1, out ra);
                    HF.Cloud.BLL.Common.Logger.Error("IntoGroup方法加入群组结果（大于0成功）：" + exeResult+ "----userUniteGroupEL.Valid:"+ userUniteGroup_Valid);
                    if (exeResult > 0&& userUniteGroup_Valid==1)
                    {
                        result = "success";
                    }
                    else if(exeResult > 0 && userUniteGroup_Valid == 2)
                    {
                        result = "secsuccess";
                    }
                    else
                    {
                        result = "error";
                    }
                }

             }
            
            return result;
        }





        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string ExitGroup(string session,string groupID)
        {
            string result = "";
            HF.Cloud.BLL.Common.Logger.Error("ExitGroup方法获取的参数session：" + session + "----groupID:" + groupID);
            //通过session获取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);//用户ID
            UserUniteGroupEL userUniteGroupEL = new UserUniteGroupEL();
            userUniteGroupEL.UserID = userID;
            userUniteGroupEL.GroupID = long.Parse(groupID);
            int ra;
            userUniteGroupEL.ExecNonQuery(2, out ra);
            HF.Cloud.BLL.Common.Logger.Error("ExitGroup方法退出群组结果（大于0成功）：" + ra);
            if (ra > 0)
            {
                result = "success";
            }
            else
            {
                result = "error";
            }
            return result;
        }



        /// <summary>
        /// 查看是否在群组中
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string IsInGroup(string session,string groupID)
        {
            string result = "";
            HF.Cloud.BLL.Common.Logger.Error("IsInGroup方法获取的参数session：" + session + "----groupID:" + groupID);
            //通过session获取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);//用户ID
            UserUniteGroupEL userUniteGroupEL = new UserUniteGroupEL();
            userUniteGroupEL.UserID = userID;
            userUniteGroupEL.GroupID = long.Parse(groupID);
            //先查看数据库中是否有记录
            DataTable dt = userUniteGroupEL.ExecDT(33);
            HF.Cloud.BLL.Common.Logger.Error("IsInGroup方法0表示不在群组中大于0表示在群组中：" + dt.Rows.Count);
            if (dt!=null&&dt.Rows.Count>0)
            {
                result = "1";
            }
            else
            {
                result = "0";
            }
            return result;
        }


        /// <summary>
        /// 获取群组人气值
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public int GetGroupPopularityNumber(long UserID)
        {
            int popuNumber = 0;
            //通过UserID获取所在的群组
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            userGroupEL.UserID = UserID;
            DataTable dt = userGroupEL.ExecDT(3);
            if(dt!=null&&dt.Rows.Count>0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    long groupID = (long)dr["GroupID"];
                    //通过所在的群组查询群组的人数，然后把群组人数累加一下
                    userGroupEL.GroupID = groupID;
                    DataTable dtGroup = userGroupEL.ExecDT(31);
                    if (dtGroup != null && dtGroup.Rows.Count > 0)
                    {
                        popuNumber += dtGroup.Rows.Count;
                    }
                }
            }
            return popuNumber;
        }

        /// <summary>
        /// 获取群组小程序码和群组名称等内容
        /// </summary>
        /// <param name="path">小程序码跳转路径</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string GetQRCodeAndGroupDetail(string path,string groupID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetQRCodeAndGroupDetail方法获取到的参数：path：" + path + "-----groupID:" + groupID);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            //获取小程序码图片的路径
            string QRCodePath = GetQRCode_Group(path, groupID);
            dic.Add("QRCodePath", QRCodePath);
            //获取群组名称和简介
            GroupEL groupEL = new GroupEL();
            groupEL.ID = long.Parse(groupID);
            groupEL.ExecuteEL(3);
            string groupName = groupEL.GroupName;
            string groupIntroduce = groupEL.Introduce.Length<20? groupEL.Introduce: groupEL.Introduce.Substring(0,20)+"...";
            dic.Add("GroupName", groupName);
            dic.Add("GroupIntroduce", groupIntroduce);
            //获取群成员公司名称（最多5个）
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            userGroupEL.GroupID = long.Parse(groupID);
            DataTable dt = userGroupEL.ExecDT(34);
            HF.Cloud.BLL.Common.Logger.Error("GetQRCodeAndGroupDetail方法获取到的公司个数：" + dt.Rows.Count);
            List<string> companyNameList = new List<string>();
            if(dt!=null&&dt.Rows.Count>0)
            {
                if (dt.Rows.Count<=5)
                {
                    for (int i=0;i<dt.Rows.Count;i++)
                    {
                        companyNameList.Add(dt.Rows[i]["CompanyName"].ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        companyNameList.Add(dt.Rows[i]["CompanyName"].ToString());
                    }
                }
            }
            dic.Add("CompanyList", companyNameList);
            string returnJS = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetQRCodeAndGroupDetail方法返回的数据：returnJS：" + returnJS);
            return returnJS;
        }


        /// <summary>
        /// 获取群组小程序码图片路径
        /// </summary>
        /// <param name="path">小程序码跳转路径</param>
        /// <param name="groupID">群组ID</param>
        /// <returns></returns>
        public string GetQRCode_Group(string path, string groupID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetQRCode_Group方法,获取的参数path：" + path+"------groupID:"+groupID);
            string imgPath = "";
            //判断数据库中是否有值
            GroupEL groupEL = new GroupEL();
            groupEL.ID = long.Parse(groupID);
            DataTable dt=groupEL.ExecDT(3);
            HF.Cloud.BLL.Common.Logger.Error("GetQRCode_Group方法,查找群组记录：" + dt.Rows.Count);
            if (dt!=null&&dt.Rows.Count>0)
            {
                //如果数据库中"QRCode"有值，则从数据库中取值
                if (!string.IsNullOrEmpty(dt.Rows[0]["QRCode"].ToString())&& dt.Rows[0]["QRCode"].ToString()!="")
                {
                    //获取图片名称 
                    string qrCodeImageName = dt.Rows[0]["QRCode"].ToString();
                    //获取图片路径 
                    string qrCodeImagePath = System.Configuration.ConfigurationManager.AppSettings["QRCodeGet_Group"];
                    imgPath = qrCodeImagePath + qrCodeImageName;
                }
                //如果没有值则请求小程序服务器获取小程序码图片，并把图片名称保存到数据库中
                else
                {
                    //获取token
                    WX_TokenBLL tokenBLL = new WX_TokenBLL();
                    string token = tokenBLL.GetToken();
                    //获取小程序码接口
                    string url = "https://api.weixin.qq.com/wxa/getwxacode?access_token=" + token;
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("path", path);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = int.MaxValue;
                    string json = js.Serialize(dic);
                    HF.Cloud.BLL.Common.Logger.Error("GetQRCode_Group方法,url：" + url + "-----Path:" + path);
                    //post返回的小程序码流
                    Stream QRCodeStream = WeChatAppDecrypt.Post(url, json);
                    //将图片流转换成图片
                    Bitmap tp = new Bitmap(QRCodeStream);
                    string QRCodeSave_Group = System.Configuration.ConfigurationManager.AppSettings["QRCodeSave_Group"];
                    string image_userName = Guid.NewGuid().ToString();
                    string qrCodeImageName = image_userName + ".jpg";
                    tp.Save(QRCodeSave_Group + qrCodeImageName);
                    //把小程序码图片名称保存到数据库中
                    groupEL.QRCode = qrCodeImageName;
                    int ra;
                    long returnValue = groupEL.ExecNonQuery(21, out ra);
                    HF.Cloud.BLL.Common.Logger.Error("GetQRCode_Group方法,保存小程序图片名称结果：" + ra);
                    if (ra > 0)
                    {
                        string QRCodeGet_Group = System.Configuration.ConfigurationManager.AppSettings["QRCodeGet_Group"];
                        imgPath = QRCodeGet_Group + qrCodeImageName;
                    }
                    else
                    {
                        imgPath = "error";
                    }
                }
            }
            else
            {
                imgPath = "error";
            }
            HF.Cloud.BLL.Common.Logger.Error("GetQRCode_Group方法,小程序码图片路径：" + imgPath);
            return imgPath;
        }



        /// <summary>
        /// 群组人员置顶 取消置顶  删除
        /// </summary>
        /// <param name="userID">人员ID</param>
        /// <param name="groupID">群组ID</param>
        /// <param name="cookie">操作的功能（top置顶distop取消置顶delete删除）</param>
        /// <returns></returns>
        public string EditUser(string session,string userID,string groupID,string cookie)
        {
            string retunStr = "";
            HF.Cloud.BLL.Common.Logger.Error("EditUser编辑群组人员方法,获取的参数userID：" + userID + "------groupID:" + groupID + "------cookie:" + cookie);
            UserUniteGroupEL userGroupEL = new UserUniteGroupEL();
            if(!string.IsNullOrEmpty(userID)&& !string.IsNullOrEmpty(groupID) && !string.IsNullOrEmpty(cookie))
            {
                userGroupEL.UserID = long.Parse(userID);
                userGroupEL.GroupID = long.Parse(groupID);
                if (cookie == "top")//置顶
                {
                    //获取当前置顶的istop最大值
                    int maxIsTop = 0;
                    DataTable dtUserGroup = userGroupEL.ExecDT(35);
                    if(dtUserGroup!=null&&dtUserGroup.Rows.Count>0)
                    {
                        maxIsTop = Int32.Parse(dtUserGroup.Rows[0]["IsTop"].ToString());
                    }
                    userGroupEL.IsTop = maxIsTop+1;
                    int ra;
                    long returnVel=userGroupEL.ExecNonQuery(22, out ra);
                    if(ra>0)
                    {
                        retunStr = GetGroupFriendList(session,groupID);
                    }
                    else
                    { retunStr = "error";}
                }
                if (cookie == "distop")//取消置顶
                {
                    userGroupEL.IsTop=0;
                    int ra;
                    long returnVel = userGroupEL.ExecNonQuery(23, out ra);
                    if (ra > 0)
                    {
                        retunStr = GetGroupFriendList(session,groupID);
                    }
                    else
                    { retunStr = "error"; }
                }
                if (cookie == "del")//删除
                {
                    //不可删自己
                    //通过groupID获取群信息
                    GroupEL groupEL = new GroupEL();
                    long groupID_long = long.Parse(groupID);
                    groupEL.ID = groupID_long;
                    groupEL.ExecuteEL(3);
                    //群主ID
                    string userID_Group = groupEL.OwnerUserID.ToString();
                    //通过sesison 获取userID
                    SB_UserEL userEL = new SB_UserEL();
                    userEL.Session_True = session;
                    DataTable dtUserEl = userEL.ExecDT(41);
                    string userID_User = "";
                    if(dtUserEl!=null&&dtUserEl.Rows.Count>0)
                    {
                        userID_User = dtUserEl.Rows[0]["ID"].ToString();
                    }
                    //如果不是群主
                    if(userID_Group!= userID_User)
                    {
                        int ra;
                        long returnVel = userGroupEL.ExecNonQuery(2, out ra);
                        if (ra < 0)
                        {
                            retunStr = "error";
                        }
                    }
                    retunStr = GetGroupFriendList(session, groupID);
                }
            }
            return retunStr;
        }



    }
}
