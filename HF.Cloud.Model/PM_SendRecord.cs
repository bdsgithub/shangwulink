using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HF.Cloud.CommonDAL;

namespace HF.Cloud.Model
{
    public class PM_SendRecord
    {
        public PM_SendRecord()
        { }
        #region Model
        private long _id;
        private long _userid;
        private string _phone="";
        private string _message = "";
        private string _reply = "";
        private int _messagetype;
        private DateTime _expiretime = DateTime.MaxValue;
        private bool _status;
        private DateTime _sendtime = DateTime.MaxValue;
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户主键ID
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Reply
        {
            set { _reply = value; }
            get { return _reply; }
        }
        /// <summary>
        /// 短信类型(1验证码2密码找回)
        /// </summary>
        public int MessageType
        {
            set { _messagetype = value; }
            get { return _messagetype; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime
        {
            set { _expiretime = value; }
            get { return _expiretime; }
        }
        /// <summary>
        /// 短信状态（1有效0无效）
        /// </summary>
        public bool Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        #endregion Model

        #region  dal
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "sp_PM_SendRecord";

        /// <summary>
        /// 执行存储过程 返回受影响行数及ID号
        /// </summary>
        /// <param name="iOperateFlag"></param>
        /// <param name="rowsAffected"></param>
        /// <returns></returns>
        public long ExecNonQuery(int iOperateFlag, out int rowsAffected)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            return DbHelperSQL.RunProcedure(proceName, sqlpc, out rowsAffected);
        }

        /// <summary>
        /// 执行存储过程 返回ID号 主要用于新增
        /// </summary>
        /// <param name="iOperateFlag"></param>
        /// <returns></returns>
        public long ExecNonQuery(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            int rowsAffected;

            return DbHelperSQL.RunProcedure(proceName, sqlpc, out rowsAffected);
        }

        /// <summary>
        /// 返回Table
        /// </summary>
        /// <returns>获取查询表</returns>
        public DataTable ExecDT(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            DataTable dt = new DataTable();

            dt = DbHelperSQL.RunProcedure(this.proceName, sqlpc, this.proceName).Tables[0];

            return dt;
        }

        /// <summary>
        /// 得到一个实体
        /// </summary>
        public void ExecuteEL(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            DataTable dt = new DataTable();

            dt = DbHelperSQL.RunProcedure(this.proceName, sqlpc, this.proceName).Tables[0];

            if (dt.Rows.Count > 0)
            {
                InitByRow(dt.Rows[0]);
            }
            else
            {
                ID = 0;
            }
        }

        /// <summary>
        /// ExecuteSqlString
        /// </summary>
        /// <returns>根据查询语言获取数据集</returns>
        public DataTable ExecuteSqlString(string SqlString)
        {
            return DbHelperSQL.Query(SqlString).Tables[0];
        }

        public int ExecuteSql(string sqlString)
        {
            return DbHelperSQL.ExecuteSql(sqlString);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderType">升序、降序，默认降序</param>
        /// <returns></returns>
        public DataSet GetPageList(int PageIndex, string strWhere, int isAllData)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@pagesize",PagerInfo.PageSize),
                new SqlParameter("@pageindex",PageIndex),
                new SqlParameter("@orderBy","ID"),
                new SqlParameter("@order","asc"),
                new SqlParameter("@filter",strWhere),
                new SqlParameter("@isAllData",isAllData),
            };
            return DbHelperSQL.RunProcedure("sp_GetAssetTypeList", parameters, "data");

        }

        /// <summary>
        /// 实体转换表
        /// </summary>
        /// <returns>由实体所填充的表，最多有一行数据</returns>
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("UserID", typeof(long));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Message", typeof(string));
            dt.Columns.Add("Reply", typeof(string));
            dt.Columns.Add("MessageType", typeof(int));
            dt.Columns.Add("ExpireTime", typeof(DateTime));
            dt.Columns.Add("Status", typeof(bool));
            dt.Columns.Add("SendTime", typeof(DateTime));

            DataRow dr = dt.NewRow();
            dr["ID"] = ID;
            dr["UserID"] = UserID;
            dr["Phone"] = Phone;
            dr["Message"] = Message;
            dr["Reply"] = Reply;
            dr["MessageType"] = MessageType;
            dr["ExpireTime"] = ExpireTime;
            dr["Status"] = Status;
            dr["SendTime"] = SendTime;
            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        ///根据数据行初始化实体
        /// </summary>
        public void InitByRow(DataRow dr)
        {
            if (null == dr) return;
            ID = (long)dr["ID"];
            UserID = (long)dr["UserID"];
            Phone = dr["Phone"].ToString();
            Message = dr["Message"].ToString();
            Reply = dr["Reply"].ToString();
            MessageType = Convert.ToInt32(dr["MessageType"].ToString());
            ExpireTime = (DateTime)dr["ExpireTime"];
            Status = (bool)dr["Status"];
            SendTime = (DateTime)dr["SendTime"];
        }

        /// <summary>
        /// 实例化SqlCommand
        /// </summary>
        /// <param name=iOperateFlag>存储过程操作标志位</param>
        /// <returns>SqlCommand</returns>
        private SqlParameterCollection InitCmd(int iOperateFlag)
        {
            SqlCommand mCmd = new SqlCommand();


            SqlParameter param;

            param = new SqlParameter("@ID", SqlDbType.BigInt);
            param.Direction = ParameterDirection.InputOutput;
            param.Value = ID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@UserID", SqlDbType.BigInt);
            param.Value = UserID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Phone", SqlDbType.VarChar);
            param.Value = Phone;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Message", SqlDbType.NVarChar);
            param.Value = Message;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Reply", SqlDbType.NVarChar);
            param.Value = Reply;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@MessageType", SqlDbType.Int);
            param.Value = MessageType;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@ExpireTime", SqlDbType.DateTime);
            param.Value = ExpireTime;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Bit);
            param.Value = Status;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@SendTime", SqlDbType.DateTime);
            param.Value = SendTime;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OperateFlag", SqlDbType.Int);
            param.Value = iOperateFlag;
            mCmd.Parameters.Add(param);

            return mCmd.Parameters;
        }
        #endregion




    }
}
