using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HF.Cloud.CommonDAL;
using System.Data;
using System.Data.SqlClient;


using HF.Cloud.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Net.Security;
using System.Xml;


namespace HF.Cloud.BLL
{
   public class CompanysBLL
    {
        /// <summary>
        /// 添加公司或获取公司ID
        /// </summary>
        /// <param name="companyName">公司名称</param>
        /// <returns>返回值是公司ID</returns>
        public string AddOrSelectCompany(string companyName)
        {
            Common.Logger.Error("AddOrSelectCompany方法获取到的参数：" + companyName);
            string companyID = "";
            //先查询公司表中是否有此公司
            CompanysEL companyEL = new CompanysEL();
            companyEL.CompanyName = companyName;
            companyEL.ExecuteEL(21);
            Common.Logger.Error("AddOrSelectCompany方法查询公司表结果：" + companyEL.ID);
            if (companyEL.ID > 0)
            {
                //如果数据库中有此公司，则返回公司ID
                companyID = companyEL.ID.ToString();
                Common.Logger.Error("AddOrSelectCompany方法调用数据库中的" + companyName + "的ID为："+ companyID);
            }
            else
            {
                //调用天眼查接口，查询公司简介
                string url_GetBriefIntroduction = "https://way.jd.com/qichacha/GetBriefIntroduction?key=736be44d18e011e6b4fb1051721d3b62&companyName=" +
                    companyName + "&dtype=json&appkey=6831311b16156e8775fff0364cb69d67";
                string getIntroduceResult = GetResponse(url_GetBriefIntroduction);
                HF.Cloud.BLL.Common.Logger.Error("第一次天眼查查询公司简介GetBriefIntroduction接口结果：" + getIntroduceResult);

                JObject objIntro = JObject.Parse(getIntroduceResult);

                //公司头像
                string companyIcon = "";
                //公司简介
                string introduce = companyName;
                try
                {
                    companyIcon = objIntro["result"]["Result"]["Logo"].ToString();
                    Common.Logger.Error("AddOrSelectCompany方法查询公司头像结果：" + objIntro["result"]["Result"]["Logo"].ToString());
               
                }
                catch { 
                    Common.Logger.Error("AddOrSelectCompany方法该公司没有logo：");
                }
                try
                {
                    //删除掉特殊字符
                    string contentStr = objIntro["result"]["Result"]["Content"].ToString();
                    string contentStr_Change = contentStr.Replace("\"", "");
                    introduce = Regex.Replace(contentStr_Change, @"[<>abcdefghijklmnopqrstuvwxyzABCDEFJHIJKLMNOPQRSTUVWXYZ/\=]", string.Empty, RegexOptions.Compiled);
                    Common.Logger.Error("AddOrSelectCompany方法首次查询公司简介结果：" + objIntro["result"]["Result"]["Content"].ToString());
                }
                catch
                {
                    //说明此接口没有公司简介或公司简介为空，那么调用另外一个接口
                    string url_detail_info = "https://way.jd.com/jindidata/detail_info?name=" +
                        companyName + "&appkey=6831311b16156e8775fff0364cb69d67";
                    string getdetail_info = GetResponse(url_detail_info);
                    Common.Logger.Error("第二次天眼查,公司简介没查到然后查询公司工商信息getdetail_info接口结果：" + url_detail_info);
                    JObject obj_detail_info = JObject.Parse(getdetail_info);
                    try
                    {
                        //删除掉特殊字符
                        string businessScopeStr = obj_detail_info["result"]["result"]["baseInfo"]["businessScope"].ToString();
                        string businessScopeStr_Change = businessScopeStr.Replace("\"", "");
                        introduce = Regex.Replace(businessScopeStr_Change, @"[<>abcdefghijklmnopqrstuvwxyzABCDEFJHIJKLMNOPQRSTUVWXYZ/\=]", string.Empty, RegexOptions.Compiled);
                        Common.Logger.Error("第二次查询公司简介结果：" + obj_detail_info["result"]["result"]["baseInfo"]["businessScope"].ToString());
                    }
                    catch
                    {
                        //如果调用2个接口都查不到公司简介，内容的话就直接显示公司名称
                        introduce = companyName;
                    }
                }
                companyEL.CompanyIcon = companyIcon;
                companyEL.Introduce = introduce;
                companyEL.CreateTime = DateTime.Now.ToString();
                companyEL.Valid = 1;
                int outValue;
                long addCompanyRes=companyEL.ExecNonQuery(1, out outValue);
                Common.Logger.Error("AddOrSelectCompany方法添加公司结果（大于0说明成功）：" + addCompanyRes);
                if (addCompanyRes>0)
                {
                    companyID = addCompanyRes.ToString();
                }
                else
                {
                    companyID = "0";
                }
            }
            return companyID;
        }


        /// <summary>
        /// 公司页面，添加或选择公司
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="companyName">公司全名称</param>
        /// <returns></returns>
        public string AddOrSelectCompany_EditCompany(string session,string companyName)
        {
            string resultStr = "";
            Common.Logger.Error("AddOrSelectCompany_EditCompany方法获取到的参数session：" + session+ "---companyName:" + companyName);
            //公司表查看数据库是否有此公司，1有的话就给session用户绑定上此公司，2没有的话就先给公司表添加公司再给session用户绑定公司
            //获取公司ID (调用AddOrSelectCompany方法即可)
            string companyID = AddOrSelectCompany(companyName);
            Common.Logger.Error("AddOrSelectCompany_EditCompany方法获取公司ID为：" + companyID);
            //通过session修改个人绑定的公司ID
            SB_UserEL userEL = new SB_UserEL();
            userEL.CompanyID = long.Parse(companyID);
            userEL.Session_True = session;
            int ra;
            long excuteUpdate= userEL.ExecNonQuery(26, out ra);
            Common.Logger.Error("AddOrSelectCompany_EditCompany方法用户绑定公司结果为（大于0成功）：" + excuteUpdate);
            if (ra>0)
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
        /// 添加关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        public string AddCompanyTag(string companyID,string tag)
        {
            HF.Cloud.BLL.Common.Logger.Error("添加公司关键字获取到参数companyID：" + companyID + "-----tag:" + tag);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //添加关键字记录
            Companys_TagEL com_TagEL = new Companys_TagEL();
            com_TagEL.CompanyID = long.Parse(companyID);
            com_TagEL.Tag = tag;
            com_TagEL.CreateTime = DateTime.Now.ToString();
            com_TagEL.Valid = 1;
            int outValue;
            long queryResult=com_TagEL.ExecNonQuery(1, out outValue);
            HF.Cloud.BLL.Common.Logger.Error("添加关键字结果（大于0说明添加成功）:" + queryResult);
            if (queryResult>0)
            {
                //获取当前公司的关键字列表
                DataTable dt = com_TagEL.ExecDT(3);
                HF.Cloud.BLL.Common.Logger.Error("获取到公司关键字个数:" + dt.Rows.Count);
                foreach(DataRow dr in dt.Rows)
                {
                    string tagName = dr["Tag"].ToString();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Tag", tagName);
                    list.Add(dic);
                }
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("返回的公司关键字json数据：" + strJson);
            return strJson;
        }



        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        public string CancelCompanyTag(string companyID, string tag)
        {
            HF.Cloud.BLL.Common.Logger.Error("删除公司关键字获取到参数companyID：" + companyID + "-----tag:" + tag);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //删除关键字记录
            Companys_TagEL com_TagEL = new Companys_TagEL();
            com_TagEL.CompanyID = long.Parse(companyID);
            com_TagEL.Tag = tag;
            int outValue;
            com_TagEL.ExecNonQuery(2, out outValue);
            HF.Cloud.BLL.Common.Logger.Error("删除关键字结果（大于0说明添加成功）:" + outValue);
            if (outValue > 0)
            {
                //获取当前公司的关键字列表
                DataTable dt = com_TagEL.ExecDT(3);
                HF.Cloud.BLL.Common.Logger.Error("删除后获取到公司关键字个数:" + dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    string tagName = dr["Tag"].ToString();
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Tag", tagName);
                    list.Add(dic);
                }
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("删除后返回的公司关键字json数据：" + strJson);
            return strJson;
        }



        /// <summary>
        /// 获取公司关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public string  GetCompanyTag(string companyID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag方法获取公司关键字获取到参数companyID：" + companyID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //关键字记录
            Companys_TagEL com_TagEL = new Companys_TagEL();
            com_TagEL.CompanyID = long.Parse(companyID);
            //获取当前公司的关键字列表
            DataTable dt = com_TagEL.ExecDT(3);
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag方法获取到公司关键字个数:" + dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                string tagName = dr["Tag"].ToString();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Tag", tagName);
                list.Add(dic);
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag方法返回的公司关键字json数据：" + strJson);
            return strJson;
        }




        /// <summary>
        /// 获取公司关键字(返回String，非键值对)(已经修改为返回键值对)
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public string GetCompanyTag_String(string companyID)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag_String方法获取公司关键字获取到参数companyID：" + companyID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            //List<string> list = new List<string>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            //关键字记录
            Companys_TagEL com_TagEL = new Companys_TagEL();
            com_TagEL.CompanyID = long.Parse(companyID);
            //获取当前公司的关键字列表
            DataTable dt = com_TagEL.ExecDT(3);
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag_String方法获取到公司关键字个数:" + dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                //string tagName = dr["Tag"].ToString();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Tag", dr["Tag"].ToString());
                list.Add(dic);
            }
            string strJson = js.Serialize(list);
            HF.Cloud.BLL.Common.Logger.Error("GetCompanyTag_String方法返回的公司关键字json数据：" + strJson);
            return strJson;
        }




        /// <summary>
        /// 获取本人公司信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public string GetMyCompanyInfo(string session)
        {
            HF.Cloud.BLL.Common.Logger.Error("GetMyCompanyInfo方法接受的参数session：" + session);
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //通过session获取CompanyID
            SB_UserEL userEl = new SB_UserEL();
            userEl.Session_True = session;
            userEl.ExecuteEL(41);
            HF.Cloud.BLL.Common.Logger.Error("GetMyCompanyInfo方法查询用户信息结果(大于0说明正确)：" + userEl.ID);
            if(userEl.ID>0)
            {
                long companyID = userEl.CompanyID;
                //通过公司ID获取公司信息
                CompanysEL companysEL = new CompanysEL();
                companysEL.ID = companyID;
                companysEL.ExecuteEL(2);
                HF.Cloud.BLL.Common.Logger.Error("GetMyCompanyInfo方法查询公司信息结果公司名称：" + companysEL.CompanyName);
                dic.Add("CompanyID", companyID);
                dic.Add("CompanyName", companysEL.CompanyName);
                dic.Add("CompanyIcon", companysEL.CompanyIcon);
                dic.Add("Introduce", companysEL.Introduce);
                //通过companyID获取公司关键字
                string companyTags = GetCompanyTag_String(companyID.ToString());
                dic.Add("Tags", companyTags);
            }

            string strJson = js.Serialize(dic);
            HF.Cloud.BLL.Common.Logger.Error("GetMyCompanyInfo方法返回的用户信息json数据：" + strJson);
            return strJson;
        }



        /// <summary>
        /// 获取好友公司信息
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <returns></returns>
        public string GetFriendCompanyInfo(string session,string companyID)
        {
            string strJson = "";
            HF.Cloud.BLL.Common.Logger.Error("GetFriendCompanyInfo方法接受的参数session:" + session + "---companyID：" + companyID);
            if (!string.IsNullOrEmpty(session) && !string.IsNullOrEmpty(companyID))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, object> dic = new Dictionary<string, object>();

                SB_UserEL userEL = new SB_UserEL();
                userEL.Session_True = session;
                userEL.ExecuteEL(41);
                string isMyCompany = "";//是否是自己公司
                if (userEL.CompanyID.ToString() == companyID)
                {
                    isMyCompany = "1";
                }
                else
                {
                    isMyCompany = "0";
                }
                long _companyID = long.Parse(companyID);
                //通过公司ID获取公司信息
                CompanysEL companysEL = new CompanysEL();
                companysEL.ID = _companyID;
                companysEL.ExecuteEL(2);
                HF.Cloud.BLL.Common.Logger.Error("GetFriendCompanyInfo方法查询公司信息结果公司名称：" + companysEL.CompanyName);
                dic.Add("CompanyID", companyID);
                dic.Add("CompanyName", companysEL.CompanyName);
                dic.Add("CompanyIcon", companysEL.CompanyIcon);
                dic.Add("Introduce", companysEL.Introduce);
                dic.Add("IsMyCompany", isMyCompany);
                //通过companyID获取公司关键字
                string companyTags = GetCompanyTag_String(companyID.ToString());
                dic.Add("Tags", companyTags);
                //3判断是否给此公司递过名片
                string userid = userEL.ID.ToString();
                //3.1查找第一个加入当前公司的用户ID
                string userid_Company = "";
                userEL.CompanyID = _companyID;
                DataTable dt_company = userEL.ExecDT(44);
                if(dt_company!=null&&dt_company.Rows.Count>0)
                {
                    userid_Company = dt_company.Rows[0]["ID"].ToString();
                }
                //3.2Notice表中查找此人是否给公司提交过名片的记录
                NoticeEL noticeEL = new NoticeEL();
                noticeEL.UserID = long.Parse(userid_Company);//第一个加入公司的人
                noticeEL.UserID_Friend = long.Parse(userid);//自己
                noticeEL.NoticeType = 1;
                DataTable dt_Notice=noticeEL.ExecDT(23);
                string isSend = "";//是否递过，0没递过，1递过
                if(dt_Notice!=null&&dt_Notice.Rows.Count>0)
                {
                    isSend = "1";
                }
                else
                {
                    isSend = "0";
                }
                dic.Add("IsSend", isSend);
                
                strJson = js.Serialize(dic);
            }
            else
            {
                strJson = "error";
            }
            HF.Cloud.BLL.Common.Logger.Error("GetFriendCompanyInfo方法返回的用户信息json数据：" + strJson);
            return strJson;
        }




        /// <summary>
        /// 给公司递名片
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public string SendCard(string session,string companyID)
        {
            string resStr = "";
            HF.Cloud.BLL.Common.Logger.Error("SendCard方法接受的参数session：" + session+"---companyID:"+companyID);
            //通过session获取UserID
            UserBLL userBLL=new UserBLL();
            long userID=userBLL.GetUserIDBySession(session);
            //通过companyID查询企业的所有员工
            SB_UserEL userEL = new SB_UserEL();
            userEL.CompanyID = long.Parse(companyID);
            DataTable dt = userEL.ExecDT(44);
            HF.Cloud.BLL.Common.Logger.Error("SendCard方法获取到企业员工数为：" + dt.Rows.Count);
            NoticeEL noticeEL = new NoticeEL();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                noticeEL.UserID = (long)dr["ID"];//接受通知用户的userID
                noticeEL.UserID_Friend = userID;//
                noticeEL.NoticeType =1;
                noticeEL.NoticeState = 0;
                noticeEL.IsLook = 0;
                noticeEL.CreateTime = DateTime.Now.ToString();
                noticeEL.Valid = 1;
                int ra;
                long notiExec = noticeEL.ExecNonQuery(1, out ra);
                if(notiExec>0)
                {
                    i = i + 1;
                }
            }
            if(dt.Rows.Count==i)
            {
                resStr = "seccess";
            }
            else
            {
                resStr = "error";
            }
            return resStr;
        }




        /// <summary>
        /// 模糊搜索公司
        /// </summary>
        /// <param name="keyword">公司关键字</param>
        /// <returns></returns>
        public string  SearchCompanyName(string keyword)
        {
            string returnStr = "";
            Common.Logger.Error("SearchCompanyName方法获取的参数keyword：" + keyword);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (!string.IsNullOrEmpty(keyword))
            {
                string url_company_list = "https://way.jd.com/jindidata/search_info4?keyword=" +
                      keyword + "&appkey=6831311b16156e8775fff0364cb69d67";
                string getdetail_company_list = GetResponse(url_company_list);
                Common.Logger.Error("SearchCompanyName方法获取的公司列表名getdetail_company_list：" + getdetail_company_list);
                JObject obj_company_list = JObject.Parse(getdetail_company_list);
                try
                {
                    JArray arrycompanys = (JArray)obj_company_list["result"]["result"]["items"];
                    for (int i = 0; i < arrycompanys.Count; i++)
                    {
                        JObject objCompany = (JObject)arrycompanys[i];
                        string companyName_First = objCompany["name"].ToString();

                        string companyName_Change = companyName_First.Replace("\"", "").Trim();
                        string companyName= Regex.Replace(companyName_Change, @"[<>abcdefghijklmnopqrstuvwxyz/\=]", string.Empty, RegexOptions.Compiled);

                        if(companyName!="")
                        {
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("name", companyName);
                            list.Add(dic);
                        }
                    }
                }
                catch
                {
                    returnStr = "error";
                }
                
                returnStr = js.Serialize(list);
                HF.Cloud.BLL.Common.Logger.Error("返回的公司名称json数据：" + returnStr);
            }
            return returnStr;
        }


        /// <summary>
        /// 修改公司简介
        /// </summary>
        /// <param name="companyContent">公司内容</param>
        /// <returns></returns>
        public string UpdateCompanyIntroduce(string companyContent)
        {
            string resultStr = "";
            BLL.Common.Logger.Error("UpdateCompanyIntroduce方法修改公司简介接受到的参数companyContent：" + companyContent);
            JavaScriptSerializer js = new JavaScriptSerializer();
            var info = js.Deserialize<Dictionary<string, object>>(companyContent);
            if (!string.IsNullOrEmpty(info["CompanyID"].ToString()))
            {
                string companyIDStr = info["CompanyID"].ToString();
                string companyIntroduceStr = info["Introduce"].ToString();

                CompanysEL companysEL = new CompanysEL();
                companysEL.ID = long.Parse(companyIDStr);
                companysEL.Introduce = companyIntroduceStr;
                int ra;
                long updateIntro=companysEL.ExecNonQuery(3, out ra);
                BLL.Common.Logger.Error("UpdateCompanyIntroduce方法修改公司简介结果（大于0成功）：" + updateIntro);
                if (ra>0)
                {
                    resultStr = "success";
                }
                else
                {
                    resultStr = "error";
                }
            }
            return resultStr;
        }
        

        /// <summary>  
        /// GET请求  
        /// </summary>  
        /// <param name="url">url地址</param>  
        /// <returns></returns>  
        private string GetResponse(string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
        




    }
}
