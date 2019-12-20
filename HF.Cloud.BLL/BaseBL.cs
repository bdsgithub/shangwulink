using System;
using System.Data.SqlClient;
using System.Data;

namespace HF.Cloud.BLL
{
    /// <summary>
    /// 软件业务规则层基础类，提供数据库连接。
    /// </summary>
    //[Guid("DDE22BA1-820B-48a8-91EB-CCAA3F4B24F8")]
    //[Transaction(TransactionOption.Required)]
    public class BaseBL// : ServicedComponent
    {
        #region 据库连接
        protected SqlTransaction trans = null;//事务变量
        public SqlConnection Conn = GetConnection(); //连接变量
        /// <summary>
        /// 获取连接池中的连接
        /// </summary>
        /// <returns></returns>
        static private SqlConnection GetConnection()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            SqlConnection conn = conn = new SqlConnection(connString);
            return conn;
        }
        /// <summary>
        /// 把用完的连接加入到池中
        /// </summary>
        /// <param name="conn"></param>
        static protected void ReleaseConnection(SqlConnection conn)
        {
            conn.Close();
        }
        #endregion

        /// <summary>
        /// 构建对象
        /// </summary>
        public BaseBL()
        {

        }        

        #region 根据查询语句获取数据表、数据集
        /// <summary>
        /// 根据查询语句获取数据集
        /// </summary>
        /// <param name="SqlString">查询语句</param>
        /// <returns>有效表</returns>
        /// <remarks>
        ///		<newpara>Write By 杨殿君 2015-11-17</newpara>
        /// </remarks>
        public DataTable GetDataTableBySqlString(string SqlString)
        {
            bool bClosed = (Conn.State == ConnectionState.Open) ? false : true;
            if (SqlString == null || SqlString == "")
            {
                return null;
            }

            DataTable dt = new DataTable();
            try
            {
                if (bClosed) Conn.Open();

                SqlCommand mCmd = new SqlCommand();
                mCmd.Connection = Conn;
                mCmd.CommandText = SqlString;
                SqlDataAdapter sqlApt = new SqlDataAdapter(mCmd);
                sqlApt.Fill(dt);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                if ((bClosed) && (Conn.State == ConnectionState.Open))
                {
                    Conn.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据查询语句获取数据集
        /// </summary>
        /// <param name="SqlString">查询语句</param>
        /// <returns>有效表</returns>
        /// <remarks>
        ///		<newpara>Write By 杨殿君 2015-11-17</newpara>
        /// </remarks>
        public DataSet GetDataSetBySqlString(string SqlString)
        {
            bool bClosed = (Conn.State == ConnectionState.Open) ? false : true;
            if (SqlString == null || SqlString == "")
            {
                return null;
            }

            DataSet ds = new DataSet();
            try
            {
                if (bClosed) Conn.Open();

                SqlCommand mCmd = new SqlCommand();
                mCmd.Connection = Conn;
                mCmd.CommandText = SqlString;
                SqlDataAdapter sqlApt = new SqlDataAdapter(mCmd);
                sqlApt.Fill(ds);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                if ((bClosed) && (Conn.State == ConnectionState.Open))
                {
                    Conn.Close();
                }
            }

            return ds;
        }
       
        #endregion        
    }
}
