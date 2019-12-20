using System.IO;
namespace HF.Cloud.CommonDAL
{
    public class Upload
    {

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        public void UploadFile(System.Web.UI.WebControls.FileUpload postedFile, string path, string filename)
        {
            #region 上传文件
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(path + "/" + filename))//如果已经存在此文件就删除该文件
            {
                File.Delete(path + "/" + filename);
            }
            postedFile.SaveAs(path + "/" + filename);//上传文件
            #endregion
        }

        /// <summary>
        /// 需要传入的参数 文件名（新命名的文件名称），上传控件的名称
        /// </summary>
        public Upload()
        {

        }

        private System.Web.HttpPostedFile postedFile = null;
        private string savePath = string.Empty;
        private string extension = string.Empty;
        private int fileLength = 20971520;
        private string setfilenme = string.Empty;

        /// <summary>
        /// 本次上传文件的名称
        /// 为空系统自动取 时间字符串作为文件的名称
        /// </summary>
        public string SetFileName
        {
            set
            {
                setfilenme = value;
            }
        }

        //显示该组件使用的参数信息
        public string Help
        {
            get
            {
                string helpstring;
                helpstring = "<font size=3>MyUpload myUpload=new MyUpload(); //构造函数";
                helpstring += "myUpload.PostedFile=file1.PostedFile;//设置要上传的文件";
                helpstring += "myUpload.SavePath=\"e:\\\";//设置要上传到服务器的路径，默认c:\\";
                helpstring += "myUpload.FileLength=100; //设置上传文件的最大长度，单位k，默认1k";
                helpstring += "myUpload.Extension=\"doc\";设置上传文件的扩展名，默认txt";
                helpstring += "label1.Text=myUpload.Upload();//开始上传，并显示上传结果</font>";
                helpstring += "<font size=3 color=red>2001-12-12 All Right Reserved!</font>";
                return helpstring;
            }
        }

        /// <summary>
        /// 上传控件
        /// </summary>
        public System.Web.HttpPostedFile PostedFile
        {
            get
            {
                return postedFile;
            }
            set
            {
                postedFile = value;
            }
        }


        /// <summary>
        /// 保存的相对路径
        /// 为空则系统自动取配置文件中的 UploadFileFolder 
        /// </summary>
        public string SavePath
        {
            get
            {
                if (!string.IsNullOrEmpty(savePath))
                {
                    return savePath;
                }
                else
                {
                    string sp = ConfigHelper.GetConfigString("UploadFileFolder");

                    if (!string.IsNullOrEmpty(sp))
                    {
                        savePath = sp;
                        return savePath;
                    }
                    else
                    {
                        return "c:\\";
                    }
                }
            }
            set
            {
                savePath = value;
            }
        }


        /// <summary>
        /// 本次上传支持的最大上传大小
        /// 默认大小20M 20*1024*1024
        /// </summary>
        public int FileLength
        {
            get
            {
                return fileLength;
            }
            set
            {
                fileLength = value;
            }
        }


        /// <summary>
        /// 本次上传支持的文件格式
        /// 默认支持的格式 txt,doc,docx,xls,xlsx,jpg,png,bmp,gif
        /// </summary>
        public string Extension
        {
            get
            {
                if (!string.IsNullOrEmpty(extension))
                {
                    return extension;
                }
                else
                {
                    return "txt,doc,docx,xls,xlsx,jpg,png,bmp,gif";
                }
            }
            set
            {
                extension = value;
            }
        }


        /// <summary>
        /// 根据路径获得上传文件的文件名称含后缀名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string PathToName(string path)
        {
            int pos = path.LastIndexOf("\\");
            return path.Substring(pos + 1);
        }



        /// <summary>
        /// 文件上传 Return -1="文件类型有问题" 0="文件超过指定大小"
        /// </summary>
        /// <returns>Return -1="文件类型有问题" 0="文件超过指定大小"</returns>
        public string UploadFile()
        {
            string strMappath = string.Empty;
            if (this.PostedFile != null || this.PostedFile.ToString() != "")
            {
                try
                {
                    string type1 = this.PostedFile.FileName.Substring(this.PostedFile.FileName.LastIndexOf(".") + 1);
                    string fileName = PathToName(PostedFile.FileName);
                    if (!string.IsNullOrEmpty(type1))
                    {
                        if (!CompareType(type1))
                        {
                            System.Web.HttpContext.Current.Response.Write("<script>alert('请上传'+'" + Extension + "'+'文件');</script>");

                            return "-1";

                        }
                        if (PostedFile.ContentLength > FileLength)
                        {
                            System.Web.HttpContext.Current.Response.Write("<script>alert('文件太大了!');</script>");
                            return "0";
                        }
                        if (!string.IsNullOrEmpty(setfilenme))
                        {
                            strMappath = this.SavePath + setfilenme + "." + type1;
                        }
                        else
                        {
                            strMappath = this.SavePath + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + type1;
                        }

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(this.SavePath)))
                        {
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(this.SavePath));
                        }

                        PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(strMappath));
                        return strMappath;
                    }
                    else
                    {
                        strMappath = "-1";
                    }
                }
                catch
                {
                    strMappath = "-1";
                }
            }
            else
            {
                return "-1";
            }
            return strMappath;
        }



        public bool CompareType(string type)
        {
            if (this.Extension.IndexOf(type) < 0)
                return false;
            else
                return true;
        }
    }
}
