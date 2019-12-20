using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace HF.Cloud.BM.SaleLabels
{
    public partial class QBModePrintSlt : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GntRandom();
                BindInputPageValue();
            }
        }

        /// <summary>
        /// 获得传入的二维码模板Id
        /// </summary>
        /// <returns></returns>
        protected long GetId()
        {
            long id = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                id = long.Parse(Request.QueryString["tid"].Trim());
            }

            return id;
        }

        protected void BindInputPageValue()
        {

            //获取当前模板前缀
            Model.QB_ModelEL model = new Model.QB_ModelEL { ID = GetId() };
            DataTable dt_Model = model.ExecDT(4);
            string strpre = "";
            if (dt_Model.Rows.Count > 0)
            {
                strpre = dt_Model.Rows[0]["ENTag"].ToString();
            }

            Model.QB_CodeEL qbCode = new Model.QB_CodeEL()
            {
                MainID = 0,
                QBCode= strpre
            };

            DataTable dt = new DataTable();

            dt = qbCode.ExecDT(59);//此服务商下面还没有打印的二维码

            if (dt != null)
            {
                this.lblNotPrintNum.Text = dt.Rows.Count.ToString();
            }
            else
            {
                this.lblNotPrintNum.Text = "0";
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns></returns>
        protected string ValidateInputValue()
        {
            string tip = string.Empty;

            if (this.txtPrintNum.Text.Trim() != string.Empty)
            {
                if (!BLL.Common.PageValidate.IsNumber(this.txtPrintNum.Text.Trim()))
                {
                    tip += "请输入正确的数字格式！";
                }
            }
            else
            {
                tip += "请输入打印数量！";
            }

            return tip;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string tips = ValidateInputValue();

            if (tips == string.Empty)
            {
                long mainId = 0;

                DataTable dt = new DataTable();

                string maxQBCode = string.Empty;
                string scpt = string.Empty;

                //前缀修改为从数据库读取
                Model.QB_ModelEL model = new Model.QB_ModelEL { ID = GetId() };
                DataTable dt_Model = model.ExecDT(4);
                string strpre = "";
                if (dt_Model.Rows.Count > 0)
                {
                    strpre = dt_Model.Rows[0]["ENTag"].ToString();
                }

                Model.QB_CodeEL qbCode = new Model.QB_CodeEL()
                {
                    QBCode = strpre   //这里QBCode只是前缀，存储过程中用来查找有此前缀的二维码
                };

                dt = qbCode.ExecDT(57);//空白标签只根据前缀来判断，空白标签的前缀任何服务商都不可以用

                if (dt != null && dt.Rows.Count > 0)//获取最大的标签
                {
                    maxQBCode = dt.Rows[0]["QBCode"].ToString();//注释掉的话就可以从0开始打印标签了
                    //maxQBCode = "wsjd00002519";//用这个可以从HF00000101开始打印标签了
                }

                if (this.rblPrintSelect.SelectedValue == "1")
                {
                    string newQBCode = string.Empty;//新的标签号
                    long startId = 0;
                    long endId = 0;

                    string qbCodeUriPath = CommonDAL.ConfigHelper.GetConfigString("QBCodeUrlPath");

                    int gnum = 0;

                    gnum = int.Parse(this.txtPrintNum.Text.Trim());

                    newQBCode = GenerateQBCode(maxQBCode);

                    for (int i = 0; i < gnum; i++)
                    {
                        if (newQBCode != string.Empty)
                        {
                            if (i == 0)
                            {
                                startId = CreatCode(qbCodeUriPath + "?mid=" + mainId + "&cid=" + newQBCode, newQBCode);
                            }
                            else if (i == gnum - 1)
                            {
                                endId = CreatCode(qbCodeUriPath + "?mid=" + mainId + "&cid=" + newQBCode, newQBCode);
                            }
                            else
                            {
                                CreatCode(qbCodeUriPath + "?mid=" + mainId + "&cid=" + newQBCode, newQBCode);
                            }

                            newQBCode = GenerateQBCode(newQBCode);
                        }
                    }

                    Model.Q_LabelStatusEL lse = new Model.Q_LabelStatusEL()
                    {
                        BatchCode = this.lblBatchNo.Text.Trim(),
                        Status = 1,
                        OperateId = UserId,
                        ChangeTime = DateTime.Now
                    };

                    lse.ExecNonQuery(1);

                    if (gnum == 1)
                    {
                        endId = startId;
                    }

                    //pt 打印类型
                    //pn 打印数量
                    if (startId > 0 && endId > 0)
                    {
                        scpt = "open('" + "QBCodePrint.aspx?sid=" + startId + "&eid=" + endId + "&pt=newprint" + "&tid=" + GetId() + "&mainId=" + mainId + "');";
                        BLL.Common.MessageBox.ResponseScript(this, scpt);
                        //BLL.Common.MessageBox.Redirect(this, "/QBCode/QBCodePrint.aspx?sid=" + startId + "&eid=" + endId + "&pt=newprint" + "&tid=" + GetId());
                    }
                }
                else if (this.rblPrintSelect.SelectedValue == "2")
                {
                    scpt = "open('" + "QBCodePrint.aspx?pn=" + this.txtPrintNum.Text.Trim() + "&pt=oldprint" + "&tid=" + GetId() + "&mainId=" + mainId + "');";
                    BLL.Common.MessageBox.ResponseScript(this, scpt);
                    //BLL.Common.MessageBox.Redirect(this, "/QBCode/QBCodePrint.aspx?pn=" + this.txtPrintNum.Text.Trim() + "&pt=oldprint" + "&tid=" + GetId());
                }
            }
            else
            {
                BLL.Common.MessageBox.Show(this, tips);
            }
        }

        /// <summary>
        /// 生成新的二维码编号
        /// </summary>
        /// <param name="qbCode"></param>
        /// <returns>前两位为字母，后面为数字</returns>
        protected string GenerateQBCode(string qbCode)
        {
            string newCode = string.Empty;
            //前缀修改为从数据库读取
            Model.QB_ModelEL model = new Model.QB_ModelEL { ID = GetId() };
            DataTable dt = model.ExecDT(4);
            string strpre = "";
            if (dt.Rows.Count > 0)
            {
                strpre = dt.Rows[0]["ENTag"].ToString();
            }

            try
            {
                if (string.IsNullOrEmpty(qbCode))
                {

                    newCode = strpre + "00000001";
                }
                else
                {
                    newCode = qbCode.Substring(strpre.Length);

                    newCode = strpre + ((long.Parse(newCode) + 1).ToString().PadLeft(8, '0'));
                }
            }
            catch (Exception er)
            {
                HF.Cloud.BLL.Common.Logger.Error(qbCode);
                HF.Cloud.BLL.Common.Logger.Error("newcode:" + newCode, er);
            }

            return newCode;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="str">二维码内容</param>
        /// <param name="fileName">二维码图片的名称 不含后缀</param>
        private long CreatCode(string str, string fileName)
        {
            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            qrCodeEncoder.QRCodeVersion = 5;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            #region 
            ////将字符串生成二维码图片
            //if (FileUpload1.PostedFile.FileName != "")
            //{
            //    System.Drawing.Image image = qrCodeEncoder.Encode(str);
            //    image = CombinImage(image, Path.GetFullPath(FileUpload1.PostedFile.FileName));

            //    image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\uploadImg\\qrImgs\\" + GUID + ".jpg");
            //}
            //else
            //{
            //}

            #endregion

            string qbCodeImagePath = CommonDAL.ConfigHelper.GetConfigString("QBCodeImagePath");

            string extension = ".jpg";

            Bitmap bim = qrCodeEncoder.Encode(str, Encoding.Default);

            string phyImgPath = Server.MapPath(qbCodeImagePath);//转换为物理地址

            if (!System.IO.Directory.Exists(phyImgPath))
            {
                System.IO.Directory.CreateDirectory(phyImgPath);
            }

            string fileAllPath = qbCodeImagePath + fileName + extension;//相对路径

            long returnId = AddNewQBCodeToTable(fileName, fileAllPath, str);//写入数据库

            if (returnId > 0)
            {

                Model.Q_LabelBatch lb = new Model.Q_LabelBatch()
                {
                    QRCode = fileName,
                    BatchCode = this.lblBatchNo.Text.Trim(),
                    TempId = GetId(),
                    ChannelId = 0,
                    Creater = UserId,
                    CreateTime = DateTime.Now
                };

                lb.ExecNonQuery(1);
                
                bim.Save(phyImgPath + fileName + extension);//保存到硬盘
            }

            return returnId;

        }

        /// <summary>
        /// 插入新的二维码记录
        /// </summary>
        /// <returns></returns>
        public long AddNewQBCodeToTable(string qbCode, string qbCodeImagePath, string QBDetail)
        {
            long mainId = 0;

            long returnId = 0;
            Model.QB_CodeEL model = new Model.QB_CodeEL()
            {
                MainID = mainId,
                QBCode = qbCode,
                //QBDetail = QBDetail,//生成的空标签 暂时不给地址，在分配标签时生成地址
                QBPath = qbCodeImagePath,
                AssetID = 0,
                PrintNum = 0,
                Valid = 1
            };

            returnId = model.ExecNonQuery(1);

            return returnId;
        }

        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框    

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }
        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        protected void GntRandom()
        {
            Random rdm = new Random(Guid.NewGuid().GetHashCode());
            this.lblBatchNo.Text = rdm.Next(10000000, 99999999).ToString();
        }

        protected void rblPrintSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblPrintSelect.SelectedIndex == 0)
            {
                GntRandom();
            }
        }
    }
}