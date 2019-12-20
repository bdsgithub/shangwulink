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
   public class HongBaoService : IHongBaoService
    {

        /// <summary>
        /// 红包开关
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public Stream OpenRedBag(string companyID)
        {
            try
            {
                HongBaoBLL hongBaoBLL = new HongBaoBLL();
                string resul = hongBaoBLL.OpenRedBag(companyID);

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
                Logger.Error("OpenRedBag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }

        /// <summary>
        /// 领红包
        /// </summary>
        /// <param name="session">用户sesison</param>
        /// <param name="companyID">发红包的公司ID</param>
        /// <param name="formID">用户提交submit表单的formID</param>
        /// <returns></returns>
        public Stream GetRedBag(string session, string companyID, string formID)
        {
            try
            {
                HongBaoBLL hongBaoBLL = new HongBaoBLL();
                string resul = hongBaoBLL.GetRedBag(session, companyID, formID);

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
                Logger.Error("GetRedBag Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


        /// <summary>
        /// 是否激活红包
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public Stream IsActive(string companyID)
        {
            try
            {
                HongBaoBLL hongBaoBLL = new HongBaoBLL();
                string resul = hongBaoBLL.IsActive(companyID);

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
                Logger.Error("IsActive Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }


    }
}
