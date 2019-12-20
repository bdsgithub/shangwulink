using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string firstStr = txtFirst.Text.ToString();
            string middle01 = firstStr.Replace("\"","");
            string middle02 = Regex.Replace(middle01, @"[<>abcdefghijklmnopqrstuvwxyzABCDEFJHIJKLMNOPQRSTUVWXYZ/\=]", string.Empty, RegexOptions.Compiled);

            txtSecond.Text = middle02;

        }

        private void btn_ll_Click(object sender, EventArgs e)
        {
            #region MyRegion

            //OpenFileDialog fileDialog = new OpenFileDialog();
            //DialogResult result = fileDialog.ShowDialog();
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    FileStream fs = File.OpenRead(fileDialog.FileName); //OpenRead
            //    int filelength = 0;
            //    filelength = (int)fs.Length; //获得文件长度 
            //    //MessageBox.Show(filelength.ToString());
            //    byte[] buffur = new byte[fs.Length];
            //    fs.Read(buffur, 0, (int)fs.Length);
            //    MessageBox.Show("byte:"+ buffur.Length);
            //    fs.Position = 0;
            //    fs.Flush();

            //    string toString= Convert.ToBase64String(buffur);
            //    MessageBox.Show(toString);
            //    txt_ll.Text = toString;


            //    MemoryStream ms = new MemoryStream(buffur);
            //    FileStream targetStream = null;
            //    using (targetStream = new FileStream(@"c:\pic.png", FileMode.Create, FileAccess.Write, FileShare.None))
            //    {
            //        const int bufferLen = 4096;
            //        byte[] buffer = new byte[bufferLen];
            //        int count = 0;
            //        while ((count = ms.Read(buffer, 0, bufferLen)) > 0)
            //        {
            //            targetStream.Write(buffer, 0, count);
            //        }
            //        targetStream.Close();
            //        ms.Close();

            //    }










            //MemoryStream ms = new MemoryStream(buffur);
            //Image img = Image.FromStream(ms);
            //img.Save(@"C:\pic.png");



            //StreamReader sr = new StreamReader(fs);
            //string ddd = sr.ReadToEnd();
            //MessageBox.Show(ddd);
            //sr.Close();
            //txt_ll.Text = ddd;





            //Byte[] image = new Byte[filelength]; //建立一个字节数组 
            //fs.Read(image, 0, filelength); //按字节流读取 
            //System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            //fs.Close();
            //Bitmap bit = new Bitmap(result);
            //return bit; 
            #endregion

        }

    }





}
