using HF.Cloud.CommonDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model.YYS
{
    //交易记录表
    public class Y_TradeRecordEL
    {
        private long m_ID;
        private long m_APPUserID;
        private string m_APPType = "";
        private string m_TradeType = "";
        private string m_TradeAmount = "";
        private long m_ActivityID;
        private long m_QuotationEnquiryID;
        private string m_OutTradeNo = "";
        private DateTime m_CreateTime = DateTime.Now;
        private int m_Valid;

        /// <summary>
        /// 主键
        /// </summary>		
        public long ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        /// <summary>
        /// APP用户ID
        /// </summary>		
        public long APPUserID
        {
            get { return m_APPUserID; }
            set { m_APPUserID = value; }
        }
        /// <summary>
        /// APP类型
        /// </summary>		
        public string APPType
        {
            get { return m_APPType; }
            set { m_APPType = value; }
        }
        /// <summary>
        /// 交易类型（0消费1充值）
        /// </summary>		
        public string TradeType
        {
            get { return m_TradeType; }
            set { m_TradeType = value; }
        }
        /// <summary>
        /// 交易金额
        /// </summary>		
        public string TradeAmount
        {
            get { return m_TradeAmount; }
            set { m_TradeAmount = value; }
        }
        /// <summary>
        /// 活动ID
        /// </summary>		
        public long ActivityID
        {
            get { return m_ActivityID; }
            set { m_ActivityID = value; }
        }
        /// <summary>
        /// 报价单ID
        /// </summary>		
        public long QuotationEnquiryID
        {
            get { return m_QuotationEnquiryID; }
            set { m_QuotationEnquiryID = value; }
        }
        /// <summary>
        ///订单号
        /// </summary>		
        public string OutTradeNo
        {
            get { return m_OutTradeNo; }
            set { m_OutTradeNo = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime
        {
            get { return m_CreateTime; }
            set { m_CreateTime = value; }
        }
        /// <summary>
        /// 有效性（1有效 0无效）
        /// </summary>		
        public int Valid
        {
            get { return m_Valid; }
            set { m_Valid = value; }
        }




        #region DAL
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "sp_Y_TradeRecord";

        /// <summary>
        /// 构造函数
        /// </summary> 
        public Y_TradeRecordEL()
        { }

        /// <summary>
        /// 执行存储过程 返回受影响行数及ID号
        /// </summary>
        /// <param name="iOperateFlag"></param>
        /// <param name="rowsAffected"></param>
        /// <returns></returns>
        public long ExecNonQuery(int iOperateFlag, out int rowsAffected)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            return DbHelperSQLYYS.RunProcedure(proceName, sqlpc, out rowsAffected);
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

            return DbHelperSQLYYS.RunProcedure(proceName, sqlpc, out rowsAffected);
        }

        /// <summary>
        /// 返回Table
        /// </summary>
        /// <returns>获取查询表</returns>
        public DataTable ExecDT(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            DataTable dt = new DataTable();

            dt = DbHelperSQLYYS.RunProcedure(this.proceName, sqlpc, this.proceName).Tables[0];

            return dt;
        }



        /// <summary>
        /// 得到一个实体
        /// </summary>
        public void ExecuteEL(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);

            DataTable dt = new DataTable();

            dt = DbHelperSQLYYS.RunProcedure(this.proceName, sqlpc, this.proceName).Tables[0];

            if (dt.Rows.Count > 0)
            {
                InitByRow(dt.Rows[0]);
            }
            else
            {
                m_ID = 0;
            }
        }

        /// <summary>
        /// ExecuteSqlString
        /// </summary>
        /// <returns>根据查询语言获取数据集</returns>
        public DataTable ExecuteSqlString(string SqlString)
        {
            return DbHelperSQLYYS.Query(SqlString).Tables[0];
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrder">排序字段</param>
        /// <param name="orderType">升序、降序，默认降序</param>
        /// <returns></returns>
        public DataSet GetPageList(string strTableName, int PageIndex, string strWhere, string strOrder, OrderType orderType = OrderType.desc)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@OrderfldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@IsReCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1000)
            };
            parameters[0].Value = strTableName;
            parameters[1].Value = "*";//根据实际情况进行修正
            parameters[2].Value = strOrder;
            parameters[3].Value = PagerInfo.PageSize;
            parameters[4].Value = PageIndex;
            parameters[5].Value = 1;
            parameters[6].Value = (int)orderType;
            parameters[7].Value = strWhere;

            return DbHelperSQLYYS.RunProcedure("UP_GetRecordByPageOrder", parameters, "dspage");
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="order">排序顺序</param>
        /// <param name="orderType">升序、降序，默认降序</param>
        /// <returns></returns>
        public DataSet XCXGetDataList(int PageSize, int PageIndex, string orderBy, string order, string strWhere, int isAllData)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@pageSize",PageSize),
                new SqlParameter("@currPage",PageIndex),
                new SqlParameter("@orderBy",orderBy),
                new SqlParameter("@order",order),
                new SqlParameter("@filter",strWhere),
                new SqlParameter("@isAllData",isAllData),
            };
            return DbHelperSQLYYS.RunProcedure("sp_S_EmbodyContactPage", parameters, "data");

        }
        /// <summary>
        /// 实体转换表
        /// </summary>
        /// <returns>由实体所填充的表，最多有一行数据</returns>
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("APPUserID", typeof(long));
            dt.Columns.Add("APPType", typeof(string));
            dt.Columns.Add("TradeType", typeof(string));
            dt.Columns.Add("TradeAmount", typeof(string));
            dt.Columns.Add("ActivityID", typeof(long));
            dt.Columns.Add("QuotationEnquiryID", typeof(long));
            dt.Columns.Add("OutTradeNo", typeof(string));
            dt.Columns.Add("CreateTime", typeof(DateTime));
            dt.Columns.Add("Valid", typeof(int));



            DataRow dr = dt.NewRow();
            dr["ID"] = m_ID;
            dr["APPUserID"] = m_APPUserID;
            dr["AppType"] = m_APPType;
            dr["TradeType"] = m_TradeType;
            dr["TradeAmount"] = m_TradeAmount;
            dr["ActivityID"] = m_ActivityID;
            dr["QuotationEnquiryID"] = m_QuotationEnquiryID;
            dr["OutTradeNo"] = m_OutTradeNo;
            dr["CreateTime"] = m_CreateTime;
            dr["Valid"] = m_Valid;

            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        ///根据数据行初始化实体
        /// </summary>
        public void InitByRow(DataRow dr)
        {
            if (null == dr) return;
            m_ID = (long)dr["ID"];
            m_APPUserID = (long)dr["APPUserID"];
            m_APPType = dr["APPType"].ToString();
            m_TradeType = dr["TradeType"].ToString();
            m_TradeAmount = dr["TradeAmount"].ToString();
            m_ActivityID = (long)dr["ActivityID"];
            m_QuotationEnquiryID = (long)dr["QuotationEnquiryID"];
            m_OutTradeNo = dr["OutTradeNo"].ToString();
            m_CreateTime = (DateTime)dr["CreateTime"];
            m_Valid = (int)dr["Valid"];

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
            param.Value = m_ID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@APPUserID", SqlDbType.BigInt);
            param.Value = m_APPUserID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@APPType", SqlDbType.VarChar, 50);
            if (m_APPType == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_APPType.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@TradeType", SqlDbType.VarChar, 50);
            if (m_TradeType == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_TradeType.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@TradeAmount", SqlDbType.VarChar, 50);
            if (m_TradeAmount == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_TradeAmount.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@ActivityID", SqlDbType.BigInt);
            param.Value = m_ActivityID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@QuotationEnquiryID", SqlDbType.BigInt);
            param.Value = m_QuotationEnquiryID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OutTradeNo", SqlDbType.VarChar, 50);
            if (m_OutTradeNo == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_OutTradeNo.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@CreateTime", SqlDbType.DateTime);
            param.Value = m_CreateTime;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Valid", SqlDbType.Bit);
            param.Value = m_Valid;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OperateFlag", SqlDbType.Int);
            param.Value = iOperateFlag;
            mCmd.Parameters.Add(param);


            return mCmd.Parameters;
        }
        #endregion




















    }
}
