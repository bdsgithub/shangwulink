using System;
using System.Data.SqlClient;
using System.Data;

namespace HF.Cloud.BLL
{
    /// <summary>
    /// ���ҵ����������࣬�ṩ���ݿ����ӡ�
    /// </summary>
    //[Guid("DDE22BA1-820B-48a8-91EB-CCAA3F4B24F8")]
    //[Transaction(TransactionOption.Required)]
    public class BaseBL// : ServicedComponent
    {
        #region �ݿ�����
        protected SqlTransaction trans = null;//�������
        public SqlConnection Conn = GetConnection(); //���ӱ���
        /// <summary>
        /// ��ȡ���ӳ��е�����
        /// </summary>
        /// <returns></returns>
        static private SqlConnection GetConnection()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
            SqlConnection conn = conn = new SqlConnection(connString);
            return conn;
        }
        /// <summary>
        /// ����������Ӽ��뵽����
        /// </summary>
        /// <param name="conn"></param>
        static protected void ReleaseConnection(SqlConnection conn)
        {
            conn.Close();
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        public BaseBL()
        {

        }        

        #region ���ݲ�ѯ����ȡ���ݱ����ݼ�
        /// <summary>
        /// ���ݲ�ѯ����ȡ���ݼ�
        /// </summary>
        /// <param name="SqlString">��ѯ���</param>
        /// <returns>��Ч��</returns>
        /// <remarks>
        ///		<newpara>Write By ���� 2015-11-17</newpara>
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
        /// ���ݲ�ѯ����ȡ���ݼ�
        /// </summary>
        /// <param name="SqlString">��ѯ���</param>
        /// <returns>��Ч��</returns>
        /// <remarks>
        ///		<newpara>Write By ���� 2015-11-17</newpara>
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
