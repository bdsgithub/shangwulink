using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace HF.Cloud.SWL
{
    /// <summary>
    /// UploadProductImage 的摘要说明
    /// </summary>
    public class UploadProductImage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string newName = Guid.NewGuid().ToString();
                string originalFilePath = file.FileName;//上传文件的路径
                string fileExtension = Path.GetExtension(originalFilePath);//获取文件的后缀
                //图片保存在服务器的地址
                //产品图片保存要加个日期文件夹
                string dataFilePath = DateTime.Now.ToString("yyyyMMdd");
                string allName = "D:/WebapiPublish/ProductImgs/" + dataFilePath+"/"+ newName + fileExtension;
                file.SaveAs(allName);
                string imagUrl = ConfigurationManager.AppSettings["ProductImgUrl"].ToString();
                string responseName = imagUrl + dataFilePath + "/" + newName + fileExtension;
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