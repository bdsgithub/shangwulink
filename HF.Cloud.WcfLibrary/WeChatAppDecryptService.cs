using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.WcfService;
using HF.Cloud.BLL;
using HF.Cloud.BLL.Common;

namespace HF.Cloud.WcfLibrary
{
    public class WeChatAppDecryptService : IWeChatAppDecryptService
    {
        /// <summary>
        /// 通过code获取微信openid和SessionKey
        /// </summary>
        /// <param name="code">login获取的code</param>
        /// <returns></returns>
        public Stream GetOpenidAndSessionKeyByCode(string code)
        {
            try
            {
                WeChatAppDecrypt wechat = new WeChatAppDecrypt();
                string openidAndSessionKey = wechat.GetOpenIdAndSessionKeyString(code);

                if (!string.IsNullOrEmpty(openidAndSessionKey))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(openidAndSessionKey));
                }
                else
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes("error"));
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetOpenidAndSessionKeyByCode Error", err);
                return new MemoryStream(Encoding.UTF8.GetBytes("error"));
            }
        }
    }
}
