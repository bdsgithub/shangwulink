using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HF.Cloud.CommonDAL;
using HF.Cloud.Model;
using System.Data;

namespace HF.Cloud.BLL
{
    public class RegistUserHandler
    {
        /// <summary>
        /// 用户注册发送验证码短信
        /// </summary>
        /// <param name="context"></param>
        public string SendMessageForLogin(string tel)
        {
            string result = "";
            string phone = tel;
           
            string messageCode = ConfigHelper.GetConfigString("Mmodel");//短短模板编码
            result=SendMessage(tel,messageCode, 1);
            
            return result;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messageCode">短信验证码代码</param>
        /// <param name="type">保存数据类型（1、注册服务商；2、重置密码）</param>
        protected string SendMessage(string tel,string messageCode, int type)
        {
            string sendresult = "";
            string phone = tel;

            string number = GetSixNumber();

            if (!string.IsNullOrEmpty(phone))
            {
                if (SaveData(number, phone, type) > 0)
                {
                    (new CommonDAL.MessageHelper()).SendValidateMessage(phone, number, messageCode);//发送短信
                    //把验证码返回出去
                    sendresult = number;
                }
                else
                {
                    sendresult = "error";
                }
            }
            else
            {
                sendresult = "empty";
            }
            return sendresult;
        }

        /// <summary>
        /// 保存验证码数据
        /// </summary>
        /// <param name="number"></param>
        /// <param name="phone"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected long SaveData(string number, string phone, int type)
        {
            Model.PM_SendRecord model = new PM_SendRecord();
            model.Message = number;
            model.MessageType = type;
            model.Phone = phone;
            model.Status = true;
            model.ExpireTime = DateTime.Now.AddMinutes(Convert.ToInt32(CommonDAL.ConfigHelper.GetConfigString("Mpasstime")));//设置失效时间
            model.SendTime = DateTime.Now;
            //把此手机的发送记录设置为无效
            model.ExecuteSql(string.Format("update PM_SendRecord set status=0 where phone='{0}'", phone));

            return model.ExecNonQuery(1);//插入发送记录

        }

        /// <summary>
        /// 获得六位随机码
        /// </summary>
        /// <returns></returns>
        protected string GetSixNumber()
        {
            return new Random().Next(100000, 1000000).ToString();
        }

        public string IsValidateCodeTrue(string phone,string code)
        {
            string res = "";
            bool flag = false;
            Model.PM_SendRecord sendRecord = new PM_SendRecord()
            {
                Phone = phone,
                Message = code
            };
            DataTable dt = new DataTable();
            dt = sendRecord.ExecDT(41);
            if (dt != null && dt.Rows.Count > 0)
            {
                flag = true;
            }
            
            if(flag)
            {
                Model.PM_SendRecord model = new PM_SendRecord();
                string sql = string.Format("select id from PM_SendRecord where status=1 and phone='{0}' and message='{1}' and ExpireTime>getdate() ", phone, code);
                DataTable dt_model = model.ExecuteSqlString(sql);

                if (dt_model.Rows.Count > 0)
                {
                    model.ID = long.Parse(dt.Rows[0]["ID"].ToString());
                    model.ExecuteEL(4);

                    model.Reply = code;
                    model.ExecNonQuery(3);//修改此条发送记录为无效
                    res = "success";
                }
                else
                {
                    res = "error";
                }
            }
            return res;
        }












    }
}
