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
    /// 公司关键字表实体
    /// </summary>
   public class Companys_TagEL
    {

        /// <summary>
        /// 主键
        /// </summary>
        private long m_ID;
        /// <summary>
        /// 公司ID
        /// </summary>
        private long m_CompanyID;
        /// <summary>
        /// 公司关键字
        /// </summary>
        private string m_Tag = "";
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
        private readonly string proceName = "SP_Companys_Tag";

        /// <summary>
        /// 构造函数
        /// </summary>
        public Companys_TagEL()
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
            dt.Columns.Add("CompanyID", typeof(long));
            dt.Columns.Add("Tag", typeof(string));
            dt.Columns.Add("CreatTime", typeof(string));
            dt.Columns.Add("Valid", typeof(int));


            DataRow dr = dt.NewRow();
            dr["ID"] = m_ID;
            dr["CompanyID"] = m_CompanyID;
            dr["Tag"] = m_Tag;
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
            m_CompanyID = (long)dr["CompanyID"];
            m_Tag = dr["Tag"].ToString();
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


            param = new SqlParameter("@CompanyID", SqlDbType.BigInt);
            param.Value = m_CompanyID;
            mCmd.Parameters.Add(param);


            param = new SqlParameter("@Tag", SqlDbType.NVarChar, 50);
            if (m_Tag == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Tag.Trim();
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
        /// 
        /// </summary>
        public long CompanyID
        {
            get
            { return this.m_CompanyID; }
            set
            { this.m_CompanyID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tag
        {
            get
            { return this.m_Tag; }
            set
            { this.m_Tag = value; }
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
