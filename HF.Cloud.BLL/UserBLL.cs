using System;
using System.Collections.Generic;
using HF.Cloud.Model;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Net;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Web;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Text;
using System.Drawing.Imaging;
using System.Linq;
using System.Collections;

namespace HF.Cloud.BLL
{
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserBLL
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="strAddUser"></param>
        /// <returns></returns>
        public string InsertUser(string strAddUser)
        {
            BLL.Common.Logger.Error("InsertUser方法新增加用户接受到的参数string：" + strAddUser);
            string InsertUserRes = "";//返回的数据
            JavaScriptSerializer js = new JavaScriptSerializer();
            var info = js.Deserialize<Dictionary<string, object>>(strAddUser);
            SB_UserEL SB_User_el = new SB_UserEL();
            
            SB_User_el.UserName = info["UserName"].ToString();
            SB_User_el.UserTel = info["UserTel"].ToString();
            SB_User_el.Duty = info["Duty"].ToString();
            SB_User_el.UserEmail = info["UserEmail"].ToString();
            SB_User_el.Detail = info["Detail"].ToString();
            SB_User_el.ImgUrl = info["ImgUrl"].ToString();
            SB_User_el.CompanyID = long.Parse(info["CompanyID"].ToString());

            SB_User_el.Popularity = 0;
            SB_User_el.Thumbs = 0;
            SB_User_el.CreateTime = DateTime.Now.ToString();
            SB_User_el.Valid = 1;
            SB_User_el.UnionID = "";

            //string strOpneID = info["OpenID"].ToString();
            //SB_User_el.OpenID = strOpneID;
            //string strSession_Key= info["Session_Key"].ToString();
            //SB_User_el.Session_Key = strSession_Key;

            //SB_User_el.UnionID = "";

            //string strSession_true = "";
            //string strRandom = new Random().Next(100000, 1000000).ToString();
            //strSession_true = strSession_Key.Substring(0, strSession_Key.Length - 2) + strOpneID + strRandom;
            //SB_User_el.Session_True = strSession_true;
            //----------------------------------------------------------------------------------
            //string code = info["Code"].ToString();
            //string strSession_true = "";
            //string strOpneID = "";
            //if (!string.IsNullOrEmpty(code))
            //{
            //    //获取openid和Session_Key
            //    WeChatAppDecrypt Wechat = new WeChatAppDecrypt();
            //    string str_res = Wechat.GetOpenIdAndSessionKeyString(code);
            //    BLL.Common.Logger.Error("新增加用户获取到openid和Sessionkey：" + str_res);
            //    var openidAndSessionKey = js.Deserialize<Dictionary<string, object>>(str_res);
            //    strOpneID = openidAndSessionKey["openid"].ToString();
            //    SB_User_el.OpenID = strOpneID;
            //    SB_User_el.Session_Key = openidAndSessionKey["session_key"].ToString();
            //    SB_User_el.UnionID = "";
            //    string strRandom = new Random().Next(100000, 1000000).ToString();
            //    strSession_true = openidAndSessionKey["session_key"].ToString() + strRandom;
            //    SB_User_el.Session_True = strSession_true;
            //}


            //string strOpneID = "";
            //string strSession_key = "";
            ////如果传过来OpenID和Session_Key（小程序获取手机号从而得到OpenID和Session_key）
            //if (!string.IsNullOrEmpty(info["OpenID"].ToString()) && !string.IsNullOrEmpty(info["Session_Key"].ToString()))
            //{
            //    strOpneID = info["OpenID"].ToString();
            //    strSession_key = info["Session_Key"].ToString();
            //}
            ////如果没有openID和Session_Key的话就用Code去换取（小程序中直接填写的手机号，没有获取）
            //else if (!string.IsNullOrEmpty(info["Code"].ToString()))
            //{
            //    //获取openid和Session_Key
            //    WeChatAppDecrypt Wechat = new WeChatAppDecrypt();
            //    string str_res = Wechat.GetOpenIdAndSessionKeyString(info["Code"].ToString());
            //    BLL.Common.Logger.Error("InsertUser方法新增加用户获取到openid和Sessionkey：" + str_res);
            //    var openidAndSessionKey = js.Deserialize<Dictionary<string, object>>(str_res);
            //    strOpneID = openidAndSessionKey["openid"].ToString();
            //    strSession_key = openidAndSessionKey["session_key"].ToString();
            //}
            //SB_User_el.OpenID = strOpneID;
            //SB_User_el.Session_Key = strSession_key;


            string session_True = info["Session_True"].ToString();
            //判断Session_True是否为空
            if (!string.IsNullOrEmpty(session_True)&& session_True!="")
            {
                //User表中判断Session_True，看是否有Session_True，有的话就是修改，没有的话就是添加
                SB_UserEL SB_User_el_IsHadSession = new SB_UserEL();
                SB_User_el_IsHadSession.Session_True = session_True;
                DataTable dt =SB_User_el_IsHadSession.ExecDT(45);
                BLL.Common.Logger.Error("InsertUser方法用户表中查询用户个数为：" + dt.Rows.Count);
            
                if(dt!=null&&dt.Rows.Count>0)
                {
                    //修改用户记录
                    SB_User_el.Session_True = session_True;
                    int aff;
                    long executeResul = SB_User_el.ExecNonQuery(24, out aff);
                    BLL.Common.Logger.Error("InsertUser方法修改用户信息结果为：" + aff);
                    if(aff > 0)
                    {
                        //返回数据库中的Session_True
                        InsertUserRes = session_True;
                        BLL.Common.Logger.Error("InsertUser方法这里是修改用户信息成功,返回的session为：" + InsertUserRes);
                    }
                    else
                    {
                        InsertUserRes = "error";
                    }
                }
                else//添加用户
                {
                    SB_User_el.Session_True = session_True;
                    //拆分sesison_True，获取openid和sesison_key
                    string str_session_key = session_True.Substring(0, 22) + "==";
                    string str_openid = session_True.Substring(22, 28);
                    SB_User_el.Session_Key = str_session_key;
                    SB_User_el.OpenID = str_openid;
                    //添加
                    int aff;
                    long executeResul = SB_User_el.ExecNonQuery(1, out aff);
                    BLL.Common.Logger.Error("InsertUser方法添加用户信息结果为：" + executeResul);
                    if (executeResul > 0)
                    {
                        InsertUserRes = session_True;
                        BLL.Common.Logger.Error("InsertUser方法添加用户信息成功！返回的session为：" + InsertUserRes);
                    }
                    else
                    {
                        InsertUserRes = "error";
                    }
                }
            }

            ////User表中判断openid，看是否有openid，有的话就是修改，没有的话就是添加
            //SB_UserEL SB_User_el_IsHaveOpenID = new SB_UserEL();
            //SB_User_el_IsHaveOpenID.OpenID = strOpneID;
            //SB_User_el_IsHaveOpenID.ExecuteEL(43);
            //if (SB_User_el_IsHaveOpenID.ID > 0)
            //{
            //    //修改用户记录
            //    int aff;
            //    long executeResul = SB_User_el.ExecNonQuery(24, out aff);
            //    if (aff > 0)
            //    {
            //        //返回数据库中的Session_True
            //        InsertUserRes = SB_User_el_IsHaveOpenID.Session_True;
            //        BLL.Common.Logger.Error("InsertUser方法这里是修改用户信息成功,返回的session为：" + InsertUserRes);
            //    }
            //    else
            //    {
            //        InsertUserRes = "";
            //    }
            //}
            //else
            //{
            //    //添加用户记录
            //    //生成session
            //    string strSession_true = "";
            //    string strRandom = new Random().Next(100000, 1000000).ToString();
            //    strSession_true = strSession_key.Substring(0, strSession_key.Length - 2) + strOpneID + strRandom;
            //    SB_User_el.Session_True = strSession_true;
            //    //添加
            //    int aff;
            //    long executeResul = SB_User_el.ExecNonQuery(1, out aff);
            //    if (executeResul > 0)
            //    {
            //        InsertUserRes = strSession_true;
            //        BLL.Common.Logger.Error("InsertUser方法添加用户信息成功！返回的session为：" + InsertUserRes);
            //    }
            //    else
            //    {
            //        InsertUserRes = "";
            //    }
            //}

            return InsertUserRes;
        }

        /// <summary>
        /// 上传头像图片
        /// </summary>
        /// <param name="strUserIcon"></param>
        /// <returns></returns>
        public string UploadUserIcon(Stream strUserIcon)
        {
            //MultipartParser mpp = new MultipartParser(strUserIcon);
            //if(mpp.MyContents.Count > 0)
            //{
            //    BLL.Common.Logger.Error("StringData:" + mpp.MyContents[0].StringData);
            //    MemoryStream ms = new MemoryStream(mpp.MyContents[0].Data);
            //    Bitmap tp = new Bitmap(ms);
            //    tp.Save("C:\\" + mpp.Filename);
            //}

            StreamReader sr = new StreamReader(strUserIcon);
            string s = sr.ReadToEnd();

            int start22 = s.IndexOf("\r\n\r\n");//找到空行位置  这种写法正确
            string headerTitle = s.Substring(0, start22 + 4);
            BLL.Common.Logger.Error("UploadUserIcon方法报文头STR：" + headerTitle);
            //报文头转换成字节
            byte[] byteArray_Title = System.Text.Encoding.Default.GetBytes(headerTitle);
            //报文头转换成字节的长度
            int byte_Title_Number = byteArray_Title.Length;
            BLL.Common.Logger.Error("UploadUserIcon方法报文头转换成字节的长度：" + byte_Title_Number);

            //获取文本内容加尾部
            string strImgs = s.Substring(start22 + 4);

            int end = strImgs.IndexOf("---");//找到末尾位置
            //找到内容
            string ContStr = strImgs.Substring(0, end - 2);
            BLL.Common.Logger.Error("UploadUserIcon方法内容Str：" + ContStr);
            //内容转换成字节 
            byte[] byteArray_Cont = System.Text.Encoding.Default.GetBytes(ContStr);
            //内容长度
            int byte_Cont_Number = byteArray_Cont.Length;
            BLL.Common.Logger.Error("UploadUserIcon方法文本的长度：" + byte_Cont_Number);

            byte[] all = System.Text.Encoding.Default.GetBytes(s);
            BLL.Common.Logger.Error("UploadUserIcon方法all长度：" + all.Length);
            byte[] bytes = new byte[all.Length];
            strUserIcon.Read(bytes, 0, all.Length);
            // 设置当前流的位置为流的开始 
            //strUserIcon.Seek(0, SeekOrigin.Begin);

            BLL.Common.Logger.Error("UploadUserIcon方法总长度：" + bytes.Length);
            BLL.Common.Logger.Error("UploadUserIcon方法流转换成字节结果：" + bytes);



            //获取到文件的byte
            byte[] byteTrue = bytes.Skip(byte_Title_Number).Take(byte_Cont_Number).ToArray();
            BLL.Common.Logger.Error("UploadUserIcon方法获取到的长度：" + byteTrue.Length);
            //MemoryStream ms_True = new MemoryStream(byteTrue);
            //StreamReader s_True = new StreamReader(ms_True);
            //string ss_True = s_True.ReadToEnd();
            //BLL.Common.Logger.Error("UploadUserIcon方法内容：" + ss_True);



            if (byteTrue.Length > 0)
            {
                string strbutter = Encoding.UTF8.GetString(byteTrue);
                FileStream fstxt = new FileStream("D:\\fstxt.png", FileMode.OpenOrCreate, FileAccess.Write);

                fstxt.Write(byteTrue, 0, byteTrue.Length);
                fstxt.Position = 0;
                fstxt.Flush();

                fstxt.Dispose();
            }








            return "success";

        }

        /// <summary>
        /// 通过session获取SB_UserEL实体
        /// </summary>
        /// <param name="session">session</param>
        /// <returns></returns>
        public SB_UserEL GetUserELBySession(string session)
        {
            SB_UserEL userEL_Return = new SB_UserEL();
            SB_UserEL userEL = new SB_UserEL();
            userEL.Session_True = session;
            userEL.ExecuteEL(41);
            if(userEL.ID>0)
            {
                userEL_Return=userEL;
            }
            return userEL_Return;
        }


        /// <summary>
        /// 通过session换取UserID主键
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public long GetUserIDBySession(string session)
        {
            long userID = 0;
            SB_UserEL userel = new SB_UserEL();
            userel.Session_True = session;
            userel.ExecuteEL(41);
            if (userel.ID > 0)
            {
                userID = userel.ID;
            }
            return userID;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public string GetUserInfoBySession(string session)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySession方法接受到的session：" + session);
            //通过session获取用户
            SB_UserEL userel = new SB_UserEL();
            userel.Session_True = session;
            userel.ExecuteEL(41);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySession方法获取用户信息结果（大于0成功）：" + userel.ID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (userel.ID > 0)
            {
                dic.Add("UserName", userel.UserName);
                dic.Add("UserTel", userel.UserTel);
                dic.Add("Duty", userel.Duty);
                dic.Add("UserEmail", userel.UserEmail);
                dic.Add("Detail", userel.Detail);
                dic.Add("ImgUrl", userel.ImgUrl);
                dic.Add("Session", userel.Session_True);
                string companyName = "";
                string companyIcon = "";
                string companyID = "";
                //通过公司ID查询公司名称
                CompanysEL compEL = new CompanysEL();
                compEL.ID = userel.CompanyID;
                compEL.ExecuteEL(2);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySession方法查询公司名称结果公司名称：" + compEL.CompanyName);
                if (!string.IsNullOrEmpty(compEL.CompanyName))
                {
                    companyName = compEL.CompanyName;
                    companyIcon = compEL.CompanyIcon;
                    companyID = compEL.ID.ToString();
                }
                dic.Add("CompanyName", companyName);
                dic.Add("CompanyIcon", companyIcon);
                dic.Add("CompanyID", companyID);
            }
            string strJson = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySession方法返回的数据为：" + strJson);
            return strJson;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID">用户userID</param>
        /// <returns></returns>
        public string GetUserInfoByUserID(string userID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoByUserID方法接受到的参数userID：" + userID);
            //通过session获取用户
            SB_UserEL userel = new SB_UserEL();
            userel.ID = long.Parse(userID);
            userel.ExecuteEL(4);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoByUserID方法获取用户姓名：" + userel.UserName);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(userel.UserName))
            {
                dic.Add("UserID", userID);
                dic.Add("UserName", userel.UserName);
                dic.Add("UserTel", userel.UserTel);
                dic.Add("Duty", userel.Duty);
                dic.Add("UserEmail", userel.UserEmail);
                dic.Add("Detail", userel.Detail);
                dic.Add("ImgUrl", userel.ImgUrl);
                dic.Add("Session", userel.Session_True);
                string companyName = "";
                string companyIcon = "";
                string companyID = "";
                //通过公司ID查询公司名称
                CompanysEL compEL = new CompanysEL();
                compEL.ID = userel.CompanyID;
                compEL.ExecuteEL(2);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoByUserID方法查询公司名称结果公司名称：" + compEL.CompanyName);
                if (!string.IsNullOrEmpty(compEL.CompanyName))
                {
                    companyName = compEL.CompanyName;
                    companyIcon = compEL.CompanyIcon;
                    companyID = compEL.ID.ToString();
                }
                dic.Add("CompanyName", companyName);
                dic.Add("CompanyIcon", companyIcon);
                dic.Add("CompanyID", companyID);
            }
            string strJson = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoByUserID方法返回的数据为：" + strJson);
            return strJson;
        }


        /// <summary>
        /// 获取用户综合信息
        /// </summary>
        /// <param name="session">本人session</param>
        /// <param name="sessionFriend">需要获取信息的用户session</param>
        /// <returns></returns>
        public string GetUserInfoBySessionAndSessionFriend(string session, string sessionFriend)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取用户信息方法获取到参数session：" + session + "-----sessionFriend:" + sessionFriend);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //通过sessionFriend获取用户
            SB_UserEL userel = new SB_UserEL();
            userel.Session_True = sessionFriend;
            userel.ExecuteEL(41);

            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取用户信息结果（大于0说明成功）：" + userel.ID);
            if (userel.ID > 0)
            {
                dic.Add("UserName", userel.UserName);
                dic.Add("UserTel", userel.UserTel);
                dic.Add("Duty", userel.Duty);
                dic.Add("UserEmail", userel.UserEmail);
                dic.Add("Detail", userel.Detail);
                dic.Add("ImgUrl", userel.ImgUrl);
                dic.Add("Session", userel.Session_True);
                #region  人气值
                //组合人气值，人气值=好友查看的次数（好友第一次查看有效，一次以上的不记录）+所有群组群友人数和
                int popularityInt = 0;//人气值
                                      //用户查看人气值
                PopularityBLL popularityBLL = new PopularityBLL();
                //通过Session换取UserID
                UserBLL userBLL = new UserBLL();
                long userID = userBLL.GetUserIDBySession(session);
                long userID_Friend = userBLL.GetUserIDBySession(sessionFriend);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend方法获取到的用户ID  userID：" + userID + "---userID_Friend:" + userID_Friend);
                int friendLookInt = popularityBLL.GetPopularityNumber(userID);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend方法用户查看人气值为：" + friendLookInt);
                popularityInt += friendLookInt;
                //群组群友人数和
                GroupBLL groupBLL = new GroupBLL();
                int groupNumber = groupBLL.GetGroupPopularityNumber(userID);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend方法群组人气值为：" + groupNumber);
                popularityInt += groupNumber;
                dic.Add("Popularity", popularityInt);//人气
                #endregion
                string companyName = "";
                //通过公司ID查询公司名称
                CompanysEL compEL = new CompanysEL();
                compEL.ID = userel.CompanyID;
                compEL.ExecuteEL(2);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取公司信息结果（公司名称为）：" + compEL.CompanyName);
                if (!string.IsNullOrEmpty(compEL.CompanyName))
                {
                    companyName = compEL.CompanyName;
                }
                dic.Add("CompanyName", companyName);
                dic.Add("CompanyIcon", compEL.CompanyIcon);
                dic.Add("CompanyID", compEL.ID);
                ///公司简介
                string companyIntroduceStr = compEL.Introduce;
                string introduceStr=companyIntroduceStr.Length < 50 ? companyIntroduceStr : companyIntroduceStr.Substring(0, 50).ToString() + "...";
                dic.Add("CompanyIntroduce", introduceStr);
                //查看是否有查看sessionFriend用户的记录，有就不用管，没有的话就在SB_Popularity添加一个记录
                bool isLook = popularityBLL.IsLooked(userID, userID_Friend);
                HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend方法，此用户是否被查看过：" + isLook);
                if (!isLook) //如果没有记录就添加一个
                {
                    long insertPopularity = popularityBLL.InsertPopularity(userID, userID_Friend);
                    HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend人气值增加结果：" + insertPopularity);
                }
            }
            //获取公司关键字
            CompanysBLL companyBLL = new CompanysBLL();
            string tags = companyBLL.GetCompanyTag_String(userel.CompanyID.ToString());
            dic.Add("Tags", tags);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取公司关键字：" + tags);
            //获取点赞个数
            ThumbsBLL thumbsBLL = new ThumbsBLL();
            string thumbs = thumbsBLL.GetThumbs(sessionFriend);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取点赞个数：" + thumbs);
            dic.Add("Thumb", thumbs.ToString());//点赞个数
            //这里要注意，第一个参数是好友的sesison，第二个参数是自己的session
            string isHadThumb = thumbsBLL.IsHadThumb(sessionFriend, session);
            dic.Add("IsHadThumb", isHadThumb);//是否被点赞
            //获取保存数
            FriendsBLL friendsBLL = new FriendsBLL();
            int saves = friendsBLL.GetSaveNumber(sessionFriend);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend获取保存个数：" + saves);
            dic.Add("Save", saves.ToString());//保存个数

            //如果session==sessionFriend，说明是自己看自己
            if (session == sessionFriend)
            {
                dic.Add("Who", "0");
            }
            else
            {
                //查看sessionFriend是否是session的好友
                bool isFriend = friendsBLL.IsFriend(session, sessionFriend);
                if (isFriend)
                {
                    //说明是好友
                    dic.Add("Who", "1");
                }
                else
                {
                    //说明不是好友
                    dic.Add("Who", "2");
                }
            }
            //小程序的token
            WX_TokenBLL tokenBLL = new WX_TokenBLL();
            string token = tokenBLL.GetToken();
            dic.Add("Token", token);

            string strJson = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetUserInfoBySessionAndSessionFriend返回的数据为：" + strJson);
            return strJson;
        }

        /// <summary>
        /// 通过code获取session
        /// </summary>
        /// <param name="code">小程序login获取的Code</param>
        /// <returns></returns>
        public string GetSessionByCode(string code)
        {
            string strJson = "";
            BLL.Common.Logger.Error("GetSessionByCode方法接受到的参数code：" + code);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string str_OpenidAndSessinKey = "";
            if (!string.IsNullOrEmpty(code))
            {
                //获取openid和Session_Key
                WeChatAppDecrypt Wechat = new WeChatAppDecrypt();
                str_OpenidAndSessinKey = Wechat.GetOpenIdAndSessionKeyString(code);

                BLL.Common.Logger.Error("GetSessionByCode方法获取到openid和Sessionkey：" + str_OpenidAndSessinKey);
                var openidAndSessionKey = js.Deserialize<Dictionary<string, object>>(str_OpenidAndSessinKey);
                string strOpenID = openidAndSessionKey["openid"].ToString();
                string strSession_Key = openidAndSessionKey["session_key"].ToString();
                //通过openID获取用户记录
                SB_UserEL userEL = new SB_UserEL();
                userEL.OpenID = strOpenID;
                DataTable dt = userEL.ExecDT(43);
                string sessionResult = "";
                string isHadRegister = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    sessionResult = dt.Rows[0]["Session_True"].ToString();
                    isHadRegister = "1";
                }
                else
                {
                    //生成session
                    //生成6位随机数
                    string strRandom = new Random().Next(100000, 1000000).ToString();
                    //session_Key格式：q/jk63En5ojUGqgi6vLHmA==（24个字符）
                    //openID格式：oFQX10O9zkRnsNYefrc48KfQWi9o（28个字符）
                    //strSession_true=strSession_Key+strOpenID+strRandom
                    //session等于sesion_key去掉后两位的“=”号(22位),加openid(28位)，加六位随机数(6位)
                    //sessionResult=q/jk63En5ojUGqgi6vLHmAoFQX10O9zkRnsNYefrc48KfQWi9o654321
                    string strSession_true = strSession_Key.Substring(0, strSession_Key.Length - 2) + strOpenID + strRandom;
                    sessionResult = strSession_true;
                    isHadRegister = "0";
                }
                dic.Add("Session", sessionResult);
                dic.Add("IsHadRegister", isHadRegister);

                strJson = js.Serialize(dic);
                HF.Cloud.BLL.Common.Logger.Error("GetSessionByCode方法返回的数据为：" + strJson);

            }
            else
            {
                strJson = "error";
            }

            return strJson;
        }


        /// <summary>
        /// 获取个人小程序码图片
        /// </summary>
        /// <param name="path">小程序码跳转的路径</param>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public string GetUserQRCode(string path,string session)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,参数path：" + path+"------session:"+session);
            string imgPath = "";
            //判断数据库中是否有值
            SB_UserEL userEL = GetUserELBySession(session);
            //如果QRCode有值，则返回数据库中QRCode的值
            if (!string.IsNullOrEmpty(userEL.QRCode) && userEL.QRCode != "")
            {
                //获取图片名称
                string qrCodeImageName = userEL.QRCode;
                //获取图片路径 
                string qrCodeImagePath = System.Configuration.ConfigurationManager.AppSettings["QRCodeGet_User"];
                imgPath = qrCodeImagePath + qrCodeImageName;
            }
            //数据库中QRCode没有数据，则需要获取小程序码图片保存图片，并把图片名称保存到数据库中
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
                HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,获取小程序码的json：" + json);
                HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,url：" + url + "-----Path:" + path);
                //post返回的小程序码流
                Stream QRCodeStream = WeChatAppDecrypt.Post(url, json);
                //将图片流转换成图片
                Bitmap tp = new Bitmap(QRCodeStream);
                string QRCodeSave_User = System.Configuration.ConfigurationManager.AppSettings["QRCodeSave_User"];
                string image_userName = Guid.NewGuid().ToString();
                string qrCodeImageName = image_userName + ".jpg";
                HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,保存小程序图片路径名称：" + QRCodeSave_User + qrCodeImageName);
                tp.Save(QRCodeSave_User + qrCodeImageName);
                //把小程序码图片名称保存到数据库中
                userEL.QRCode = qrCodeImageName;
                int ra;
                long returnValue=userEL.ExecNonQuery(27, out ra);
                HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,保存小程序图片名称结果：" +ra);
                if (ra>0)
                {
                    string QRCodeGet_User = System.Configuration.ConfigurationManager.AppSettings["QRCodeGet_User"];
                    imgPath = QRCodeGet_User + qrCodeImageName;
                }
                else
                {
                    imgPath = "error";
                }
            }
            HF.Cloud.BLL.Common.Logger.Error("GetUserQRCode方法,返回的结果：" + imgPath);
            return imgPath;
        }




    }
}
