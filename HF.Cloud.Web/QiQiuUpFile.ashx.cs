using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace HF.Cloud.Web
{
    /// <summary>
    /// QiQiuUpFile 的摘要说明
    /// </summary>
    public class QiQiuUpFile : IHttpHandler
    {
        private static string imgUrRL = "https://cdn.shangwulink.com/";
        public void ProcessRequest(HttpContext context)
        {
            //获取虚拟目录的物理路径。
            UpFileResult upFileResult = new UpFileResult();
            //string imgurls = "";
            //BLL.Common.Logger.Info("Count：" + context.Request.Files.Count);
            //foreach (HttpPostedFile file in context.Request.Files)
            //{
                 HttpPostedFile file = context.Request.Files[0];
                string fileName = file.FileName;
                string ser = context.Request.FilePath;
                Settings.AccessKey = "2qvZqxY40jmIKg5eLip0NCNd0pV4H3PDcPYuDA5M";
                //设置SK
                Settings.SecretKey = "9E7vAfkreDnXuhgfkW_OoeX5DY-bMzIVBPhv7Buo";
                try
                {
                    Mac mac = new Mac(Settings.AccessKey, Settings.SecretKey);
                    string suffixsl = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                Random rd = new Random();
                string newName = Guid.NewGuid().ToString();
                //string saveKey = "qiniu" +rd.Next(100,999)+ DateTime.Now.ToString("yyyyMMddHHmmss") + "." + suffixsl;
                string saveKey = "qiniu" + rd.Next(100, 999) + newName + "." + suffixsl;
                BLL.Common.Logger.Info("saveKey：" + saveKey);
                    //string allName = AppDomain.CurrentDomain.BaseDirectory + "/upload/" + newfileName + "." + suffixsl;
                    //file.SaveAs(allName);
                    string allName = "/UploadQiNiuFiles/" + saveKey;
                    file.SaveAs(System.Web.HttpContext.Current.Server.MapPath(allName));
                    // 本地文件路径
                    string filePath = fileName;
                    filePath = System.Web.HttpContext.Current.Server.MapPath("/UploadQiNiuFiles/" + saveKey);
                    BLL.Common.Logger.Info("filePath：" + filePath);
                    // filePath = "D:\\POS.jpg";
                    // 存储空间名
                    string Bucket = "pic-set";
                    // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
                    PutPolicy putPolicy = new PutPolicy();
                    // 设置要上传的目标空间
                    putPolicy.Scope = Bucket;
                    // 上传策略的过期时间(单位:秒)
                    putPolicy.SetExpires(3600);
                    //// 文件上传完毕后，在多少天后自动被删除
                    //putPolicy.DeleteAfterDays = 1;
                    // 生成上传token
                    string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                    BLL.Common.Logger.Info("token：" + token);
                    Config config = new Config();
                    // 设置上传区域
                    config.Zone = Zone.ZONE_CN_East;
                    // 设置 http 或者 https 上传
                    config.UseHttps = true;
                    config.UseCdnDomains = true;
                    config.ChunkSize = ChunkUnit.U512K;
                    // 表单上传
                    FormUploader target = new FormUploader(config);
                    HttpResult result = target.UploadFile(filePath, saveKey, token, null);
                    BLL.Common.Logger.Info("QiQiuUpFile，result.Code：" + result.Code);
                    BLL.Common.Logger.Info("imgUrRL + saveKey01：" + imgUrRL + saveKey);
                    if (result.Code == 200)
                    {

                        upFileResult.FileName = saveKey;
                        upFileResult.imgAllUrl = imgUrRL + saveKey;
                        upFileResult.Result = true;
                        upFileResult.ErrorMessage = "上传成功";
                    }
                    else
                    {
                        upFileResult.Result = false;
                        upFileResult.ErrorMessage = "上传失败请稍后重试!";
                    }
                    BLL.Common.Logger.Info("imgUrRL + saveKey：" + imgUrRL + saveKey);
                    BLL.Common.Logger.Info("upFileResult：" + new JavaScriptSerializer().Serialize(upFileResult));
                    DeleteFile(filePath);
                    //context.Response.Write(new JavaScriptSerializer().Serialize(upFileResult));
                    string imgurl = imgUrRL + saveKey;

                    //imgurls += imgurl + ",";
                    context.Response.Write(imgurl);

                }
                catch (Exception ex)
                {
                    upFileResult.Result = false;
                    upFileResult.ErrorMessage = ex.Message;
                    BLL.Common.Logger.Info("ex.Message：" + ex.Message);
                    BLL.Common.Logger.Info("ex：" + ex);
                }
            //}
            //context.Response.Write(imgurls);


        }

        public class UpFileResult   //: AshxCommonResult
        {
            public bool Result { get; set; }
            public string ErrorMessage { get; set; }
            public string FileName { get; set; }
            public string imgAllUrl { get; set; }
        }
        /// <summary>
        /// 根据路径删除文件
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(path);
            }
        }
        public static class Settings
        {
            public static string AccessKey { get; set; }
            public static string SecretKey { get; set; }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }












    }
}