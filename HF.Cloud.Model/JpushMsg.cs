using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model
{
    public class JpushMsg
    {
        private string sendno;//编号

        public string Sendno
        {
            get { return sendno; }
            set { sendno = value; }
        }
        private string msg_id;//信息编号

        public string Msg_id
        {
            get { return msg_id; }
            set { msg_id = value; }
        }
        private string errcode;//返回码

        public string Errcode
        {
            get { return errcode; }
            set { errcode = value; }
        }
        private string errmsg;//错误信息

        public string Errmsg
        {
            get { return errmsg; }
            set { errmsg = value; }
        }
    }
}
