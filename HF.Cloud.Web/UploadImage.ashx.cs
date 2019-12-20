using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;


namespace HF.Cloud.Web
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string newName = Guid.NewGuid().ToString();
                string originalFilePath = file.FileName;//上传文件的路径
                BLL.Common.Logger.Info("UploadImage上传的图片路径：" + originalFilePath);
                string fileExtension = Path.GetExtension(originalFilePath);//获取文件的后缀
                //图片保存在服务器的地址
                string allName = "D:/WebapiPublish/Images/" + newName + fileExtension;
                BLL.Common.Logger.Info("UploadImage保存到服务器的路径：" + allName);
                file.SaveAs(allName);
                //string imagUrl = ConfigurationManager.AppSettings["ImgUrl"].ToString();
                string imagUrl = "https://shangwulink.com/Images/";
                //BLL.Common.Logger.Info("UploadImage回复的图片路径imagUrl：" + imagUrl);
                //BLL.Common.Logger.Info("UploadImage回复的图片路径newName：" + newName);
                //BLL.Common.Logger.Info("UploadImage回复的图片路径fileExtension：" + fileExtension);
                string responseName = imagUrl + newName + fileExtension;

                BLL.Common.Logger.Info("UploadImage回复的图片路径：" + responseName);
                context.Response.Write(responseName);
                //回复的图片路径样式
                //https://shangwulink.com/Images/0016e59e-41e2-4c34-9b61-c73ffac9e0b3.jpg
            }
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