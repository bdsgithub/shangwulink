using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace HF.Cloud.BLL.Common
{
    public class Mail
    {
        public void SendMail(string sendAdd, string sendPwd, string reciveAdd, string usercode)
        {

        }
        /// <summary>
        /// 发送账号激活邮件
        /// </summary>
        /// <param name="sendAdd"></param>
        /// <param name="sendPwd"></param>
        /// <param name="reciveAdd"></param>
        /// <param name="usercode"></param>
        public void SendRegionMail(string sendAdd, string sendPwd, string reciveAdd, string usercode)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div>感谢您使用易维客！</div>");
            body.Append(string.Format("<div>您的管理员账号为：{0}</div>", usercode));
            body.Append("<div>请点击下面的链接激活您的账号完成注册。（请在48小时内完成激活）</div>");
            body.Append(string.Format("<div>http://e.eweic.com/Activated.aspx?usercode={0}</div>", new Common.Symmetric().Encrypto(usercode)));
            body.Append("<div>如果该链接无法打开，请直接拷贝以上链接到浏览器地址栏中访问。</div>");
            body.Append("<hr>");
            body.Append("<div>此信由易维客系统发出，系统不接收回信，请勿回复。</div>");

            Send("激活邮件", body, sendAdd, sendPwd, reciveAdd);
        }
        /// <summary>
        /// 发送账号激活邮件(客户)
        /// </summary>
        /// <param name="sendAdd"></param>
        /// <param name="sendPwd"></param>
        /// <param name="reciveAdd"></param>
        /// <param name="usercode"></param>
        public void SendRegionMailCustomer(string sendAdd, string sendPwd, string reciveAdd, string usercode)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<div>感谢您使用易维客！</div>");
            body.Append(string.Format("<div>您的数据激活码为：{0}</div>", usercode));
            body.Append("<div>请点击下面的链接激活您的账号完成注册。（请在48小时内完成激活）</div>");
            body.Append(string.Format("<div>http://e.eweic.com/Activated.aspx?type=customer&usercode={0}</div>", new Common.Symmetric().Encrypto(usercode)));
            body.Append("<div>如果该链接无法打开，请直接拷贝以上链接到浏览器地址栏中访问。</div>");
            body.Append("<hr>");
            body.Append("<div>此信由易维客系统发出，系统不接收回信，请勿回复。</div>");

            Send("激活邮件", body, sendAdd, sendPwd, reciveAdd);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="sendAdd"></param>
        /// <param name="sendPwd"></param>
        /// <param name="reciveAdd"></param>
        /// <returns></returns>
        public bool Send(string title, StringBuilder body, string sendAdd, string sendPwd, string reciveAdd)
        {
            // 设置发送方的邮件信息
            string smtpServer = "smtp.mxhichina.com"; //SMTP服务器
            string mailFrom = sendAdd; //登陆用户名
            string userPassword = sendPwd;//登陆密码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置        
            MailMessage mailMessage = new MailMessage(mailFrom, reciveAdd); // 发送人和收件人
            mailMessage.Subject = title;//主题
            mailMessage.Body = body.ToString();//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.High;//优先级

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
        }
    }
}
