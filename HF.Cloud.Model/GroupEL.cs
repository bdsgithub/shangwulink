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
    /// 群组实体表 
    /// </summary>
  public  class GroupEL
    {
        
        /// <summary>
        /// 主键
        /// </summary>
        private long m_ID;
        /// <summary>
        /// 群名称 
        /// </summary>
        private string m_GroupName="";
        /// <summary>
        /// 群简介 
        /// </summary>
        private string m_Introduce="";
        /// <summary>
        /// 群主 创建者UserID
        /// </summary>
        private long m_OwnerUserID;
        /// <summary>
        /// 创建时间
        /// </summary>
        private string m_CreateTime = "";
        /// <summary>
        /// 口令
        /// </summary>
        private string m_GroupPassword = "";
        /// <summary>
        /// 经度
        /// </summary>
        private string m_Lon = "";
        /// <summary>
        /// 纬度
        /// </summary>
        private string m_Lat = "";
        /// <summary>
        /// 群组小程序码
        /// </summary>
        private string m_QRCode = "";
        /// <summary>
        /// 1公开  0私密
        /// </summary>
        private int m_IsOpen;
        /// <summary>
        /// 有效性 
        /// </summary>
        private int m_Valid;


        #region DAL
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "SP_Group";

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupEL()
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
            dt.Columns.Add("GroupName", typeof(string));
            dt.Columns.Add("Introduce", typeof(string));
            dt.Columns.Add("OwnerUserID", typeof(long));
            dt.Columns.Add("CreateTime", typeof(string));
            dt.Columns.Add("GroupPassword", typeof(string));
            dt.Columns.Add("Lon", typeof(string));
            dt.Columns.Add("Lat", typeof(string));
            dt.Columns.Add("QRCode", typeof(string));
            dt.Columns.Add("IsOpen", typeof(int));
            dt.Columns.Add("Valid", typeof(int));


            DataRow dr = dt.NewRow();
            dr["ID"] = m_ID;
            dr["GroupName"] = m_GroupName;
            dr["Introduce"] = m_Introduce;
            dr["OwnerUserID"] = m_OwnerUserID;
            dr["CreateTime"] = m_CreateTime;
            dr["GroupPassword"] = m_GroupPassword;
            dr["Lon"] = m_Lon;
            dr["Lat"] = m_Lat;
            dr["QRCode"] = m_QRCode;
            dr["IsOpen"] = m_IsOpen;
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
            m_GroupName = dr["GroupName"].ToString();
            m_Introduce = dr["Introduce"].ToString();
            m_OwnerUserID = (long)dr["OwnerUserID"];
            m_CreateTime = dr["CreateTime"].ToString();
            m_GroupPassword = dr["GroupPassword"].ToString();
            m_Lon = dr["Lon"].ToString();
            m_Lat = dr["Lat"].ToString();
            m_QRCode = dr["QRCode"].ToString();
            m_IsOpen = (int)dr["IsOpen"];
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

            param = new SqlParameter("@GroupName", SqlDbType.NVarChar, 50);
            if (m_GroupName == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_GroupName.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Introduce", SqlDbType.NVarChar, 100);
            if (m_Introduce == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Introduce.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OwnerUserID", SqlDbType.BigInt);
            param.Value = m_OwnerUserID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@CreateTime", SqlDbType.VarChar, 20);
            if (m_CreateTime == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_CreateTime.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@GroupPassword", SqlDbType.VarChar, 10);
            if (m_GroupPassword == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_GroupPassword.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Lon", SqlDbType.VarChar, 100);
            if (m_Lon == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Lon.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Lat", SqlDbType.VarChar, 100);
            if (m_Lat == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Lat.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@QRCode", SqlDbType.VarChar, 200);
            if (m_QRCode == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_QRCode.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@IsOpen", SqlDbType.Int);
            param.Value = m_IsOpen;
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
        /// 群名称
        /// </summary>
        public string GroupName
        {
            get
            { return this.m_GroupName; }
            set
            { this.m_GroupName = value; }
        }
        /// <summary>
        /// 群简介
        /// </summary>
        public string Introduce
        {
            get
            { return this.m_Introduce; }
            set
            { this.m_Introduce = value; }
        }
        /// <summary>
        /// 群主ID
        /// </summary>
        public long OwnerUserID
        {
            get
            { return this.m_OwnerUserID; }
            set
            { this.m_OwnerUserID = value; }
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
        /// 加入群密码
        /// </summary>
        public string GroupPassword
        {
            get
            { return this.m_GroupPassword; }
            set
            { this.m_GroupPassword = value; }
        }
        /// <summary>
        ///经度
        /// </summary>
        public string Lon
        {
            get
            { return this.m_Lon; }
            set
            { this.m_Lon = value; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat
        {
            get
            { return this.m_Lat; }
            set
            { this.m_Lat = value; }
        }
        /// <summary>
        /// 群组 小程序码
        /// </summary>
        public string QRCode
        {
            get
            { return this.m_QRCode;}
            set
            { this.m_QRCode = value;}
        }
        /// <summary>
        /// 1公开 0私密
        /// </summary>
        public int IsOpen
        {
            get
            {
                return this.m_IsOpen;
            }
            set
            {
                this.m_IsOpen = value;
            }
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
