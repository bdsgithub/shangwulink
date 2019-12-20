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
    public class CompanysService : ICompanysService
    {
       

        /// <summary>
        /// 添加公司或查询获取公司ID
        /// </summary>
        /// <param name="companyName">公司名称</param>
        /// <returns>返回值是公司ID</returns>
        public Stream AddOrSelectCompany(string companyName)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string compID = compBLL.AddOrSelectCompany(companyName);

                if (!string.IsNullOrEmpty(compID))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(compID));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("AddOrSelectCompany Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 公司页面，添加或选择公司
        /// </summary>
        /// <param name="session">用户session</param>
        /// <param name="companyName">公司全名称</param>
        /// <returns></returns>
        public Stream AddOrSelectCompany_EditCompany(string session, string companyName)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string resultStr = compBLL.AddOrSelectCompany_EditCompany(session,companyName);

                if (!string.IsNullOrEmpty(resultStr))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(resultStr));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("AddOrSelectCompany_EditCompany Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        public Stream AddCompanyTag(string companyID, string tag)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.AddCompanyTag(companyID,tag);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("AddCompanyTag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 删除关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="tag">关键字名称</param>
        /// <returns>返回关键字列表</returns>
        public Stream CancelCompanyTag(string companyID, string tag)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.CancelCompanyTag(companyID, tag);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("CancelCompanyTag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 获取公司关键字
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public Stream GetCompanyTag(string companyID)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.GetCompanyTag(companyID);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetCompanyTag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 获取本人公司信息
        /// </summary>
        /// <param name="session">用户session</param>
        /// <returns></returns>
        public Stream GetMyCompanyInfo(string session)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.GetMyCompanyInfo(session);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetCompanyTag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 获取好友公司信息
        /// </summary>
        /// <param name="companyID">companyID</param>
        /// <returns></returns>
        public Stream GetFriendCompanyInfo(string session, string companyID)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.GetFriendCompanyInfo(session, companyID);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetCompanyTag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 给公司递名片
        /// </summary>
        /// <param name="session">session</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public Stream SendCard(string session, string companyID)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.SendCard(session,companyID);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("SendCard Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }





        /// <summary>
        /// 模糊搜索公司
        /// </summary>
        /// <param name="keyword">公司关键字</param>
        /// <returns></returns>
        public Stream SearchCompanyName(string keyword)
        {
            try
            {
                CompanysBLL compBLL = new CompanysBLL();
                string strResult = compBLL.SearchCompanyName(keyword);

                if (!string.IsNullOrEmpty(strResult))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(strResult));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("SearchCompanyName Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 修改公司简介
        /// </summary>
        /// <param name="companyContent">公司内容</param>
        /// <returns></returns>
        public Stream UpdateCompanyIntroduce(Stream stream)
        {
            string resultStr = "";
            if (stream != null)
            {
                StreamReader sr = new StreamReader(stream);
                string s = sr.ReadToEnd();
                sr.Dispose();
                CompanysBLL bl = new CompanysBLL();
                try
                {
                    resultStr = bl.UpdateCompanyIntroduce(s);
                }
                catch (Exception err)
                {
                    HF.Cloud.BLL.Common.Logger.Error("UpdateCompanyIntroduce Error", err);
                }
            }
            if (resultStr != "")
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(resultStr));
            }
            else
            {
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        



    }
}
