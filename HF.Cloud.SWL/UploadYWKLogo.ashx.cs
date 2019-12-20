using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace HF.Cloud.SWL
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadYWKLogo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string newName = Guid.NewGuid().ToString();
                //string suffixName = file.FileName.Substring(file.FileName.Length - 3);
                string originalFilePath = file.FileName;//上传文件的路径
                string fileExtension = Path.GetExtension(originalFilePath);//获取文件的后缀
                //图片保存在服务器的地址
                string allName = "D:/WebapiPublish/YWKLogoImg/" + newName+ fileExtension;
                file.SaveAs(allName);
                string imagUrl = ConfigurationManager.AppSettings["YWKLogo"].ToString();
                string responseName = imagUrl + newName + fileExtension;
                context.Response.Write(responseName);

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