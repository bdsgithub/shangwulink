using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HF.Cloud.BM.Common
{
    public partial class TicketAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //获取激活码
                string randmStr = GenerateRandomNumber(12);
                
                randmStr=ReGetRandomNumber(12, randmStr);

                lbActiveCode.Text = randmStr;
                hf_ActiveCode.Value = randmStr;
                
             }
        }


        protected void lnkBtnSearch_Click(object sender, EventArgs e)
        {
            Model.Pay.PO_MainTicket mainTicket = new Model.Pay.PO_MainTicket();
            mainTicket.ActiveCode = hf_ActiveCode.Value;
            mainTicket.ActiveMoney = decimal.Parse(txtMoney.Text);
            mainTicket.StartTime = System.DateTime.Now.ToString();
            mainTicket.EndTime = txtEndTime.Text.ToString();
            mainTicket.MainID = 0;
            mainTicket.IsActive = false;
            mainTicket.Valid = true;
            long add_result= mainTicket.ExecNonQuery(1);//添加兑换券
            if(add_result>0)
            {
                Response.Redirect("TicketList.aspx");
            }
        }


        protected void lnkBtnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketList.aspx");
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="Length">随机数长度</param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length)
        {
            char[] constant =
            {
                '0',
                //'1',
                '2','3','4','5','6','7','8','9',
                //'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H',
                //'I',
                'J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
              };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(34);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(34)]);
            }
            return newRandom.ToString();
        }


        /// <summary>
        /// 递归 验证数据库中是否有此随机数，有的话重新获取
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <param name="randomStr">当前要验证的随机数</param>
        /// <returns></returns>
        public string ReGetRandomNumber(int length,string randomStr)
        {
            string newRandomStr = randomStr;
            Model.Pay.PO_MainTicket mainTicket = new Model.Pay.PO_MainTicket();
            mainTicket.ActiveCode = randomStr;
            DataTable ticket_data = mainTicket.ExecDT(4);
            if (ticket_data.Rows.Count > 0)//如果存在重复的，则再次获取激活码
            {
                newRandomStr = GenerateRandomNumber(12);
                mainTicket.ActiveCode = newRandomStr;
                DataTable newticket_data = mainTicket.ExecDT(4);
                if (newticket_data.Rows.Count > 0)//如果存在重复的，则再次获取激活码
                {
                    ReGetRandomNumber(length, newRandomStr);
                }
            }
            return newRandomStr;
        }

    }
}