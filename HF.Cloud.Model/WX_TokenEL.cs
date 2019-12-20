using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HF.Cloud.CommonDAL;
using System.Data;
using System.Data.SqlClient;

namespace HF.Cloud.Model
{
    /// <summary>
    /// 小程序token实例
    /// </summary>
  public  class WX_TokenEL
    {

        /// <summary>
        /// 主键
        /// </summary>
        private long m_ID;
        /// <summary>
        /// 小程序token
        /// </summary>
        private string m_Token="";
        /// <summary>
        /// 创建时间
        /// </summary>
        private string m_CreateTime = "";
        /// <summary>
        /// 有效性 
        /// </summary>
        private int m_Valid;


        #region DAL
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "SP_WX_Token";

        /// <summary>
        /// 构造函数
        /// </summary> 
        public WX_TokenEL()
        {

        }

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
                m_ID = 0;
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

            return DbHelperSQL.RunProcedure("UP_GetRecordByPageOrder", parameters, "dspage");
        }

        /// <summary>
        /// 实体转换表
        /// </summary>
        /// <returns>由实体所填充的表，最多有一行数据</returns>
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("Token", typeof(string));
            dt.Columns.Add("CreateTime", typeof(string));
            dt.Columns.Add("Valid", typeof(int));


            DataRow dr = dt.NewRow();
            dr["ID"] = m_ID;
            dr["Token"] = m_Token;
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
            m_Token = dr["Token"].ToString();
            m_CreateTime = dr["CreateTime"].ToString();
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

            param = new SqlParameter("@Token", SqlDbType.VarChar, 1000);
            if (m_Token == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Token.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@CreateTime", SqlDbType.VarChar, 20);
            if (m_CreateTime == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_CreateTime.Trim();
            mCmd.Parameters.Add(param);


            param = new SqlParameter("@Valid", SqlDbType.Int);
            param.Value = m_Valid;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OperateFlag", SqlDbType.Int);
            param.Value = iOperateFlag;
            mCmd.Parameters.Add(param);


            return mCmd.Parameters;
        }
        #endregion


        #region 属性
        /// <summary>
        /// 主键 - 主键
        /// </summary>
        public long ID
        {
            get
            { return this.m_ID; }
            set
            { this.m_ID = value; }
        }
        /// <summary>
        /// 小程序token
        /// </summary>
        public string Token
        {
            get
            { return this.m_Token; }
            set
            { this.m_Token = value; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public string CreateTime
        {
            get
            { return this.m_CreateTime; }
            set
            { this.m_CreateTime = value; }
        }
        /// <summary>
        /// 有效性0无效1有效
        /// </summary>
        public int Valid
        {
            get
            {
                return this.m_Valid;
            }
            set
            {
                this.m_Valid = value;
            }
        }
        #endregion


    }
}
