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
    /// 用户表实体
    /// </summary>
    public class SB_UserEL
    {
        /// <summary>
        /// 主键
        /// </summary>
        private long m_ID;
        /// <summary>
        /// 用户名
        /// </summary>
        private string m_UserName="";
        /// <summary>
        /// 电话
        /// </summary>
        private string m_UserTel = "";
        /// <summary>
        /// 职务
        /// </summary>
        private string m_Duty = "";
        /// <summary>
        /// 用户邮箱
        /// </summary>
        private string m_UserEmail = "";
        /// <summary>
        /// 介绍
        /// </summary>
        private string m_Detail = "";
        /// <summary>
        /// 头像url
        /// </summary>
        private string m_ImgUrl = "";

        /// <summary>
        /// 公司id
        /// </summary>
        private long m_CompanyID;
        /// <summary>
        /// 小程序openid
        /// </summary>
        private string m_OpenID = "";
        /// <summary>
        /// 小程序Session_Key
        /// </summary>
        private string m_Session_Key = "";
        /// <summary>
        /// 小程序UnionID
        /// </summary>
        private string m_UnionID = "";
        /// <summary>
        /// 小程序通信Session
        /// </summary>
        private string m_Session_True = "";
        /// <summary>
        /// 人气
        /// </summary>
        private int m_Popularity;
        /// <summary>
        /// 点赞数
        /// </summary>
        private int m_Thumbs;
        /// <summary>
        /// 小程序码
        /// </summary>
        private string m_QRCode = "";
        /// <summary>
        /// 时间
        /// </summary>
        private string m_CreateTime="";
        /// <summary>
        /// 有效性 
        /// </summary>
        private int m_Valid;


        #region DAL
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "SP_SB_User";

        /// <summary>
        /// 构造函数
        /// </summary>
        public SB_UserEL()
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
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("UserTel", typeof(string));
            dt.Columns.Add("Duty", typeof(string));
            dt.Columns.Add("UserEmail", typeof(string));
            dt.Columns.Add("Detail", typeof(string));
            dt.Columns.Add("ImgUrl", typeof(string));
            dt.Columns.Add("CompanyID", typeof(long));

            dt.Columns.Add("OpenID", typeof(string));
            dt.Columns.Add("Session_Key", typeof(string));
            dt.Columns.Add("UnionID", typeof(string));
            dt.Columns.Add("Session_True", typeof(string));
            dt.Columns.Add("Popularity", typeof(int));
            dt.Columns.Add("Thumbs", typeof(int));
            dt.Columns.Add("QRCode", typeof(string));
            dt.Columns.Add("CreateTime", typeof(string));
            dt.Columns.Add("Valid", typeof(int));
            

            DataRow dr = dt.NewRow();
            dr["ID"] = m_ID;
            dr["UserName"] = m_UserName;
            dr["UserTel"] = m_UserTel;
            dr["Duty"] = m_Duty;
            dr["UserEmail"] = m_UserEmail;
            dr["Detail"] = m_Detail;
            dr["ImgUrl"] = m_ImgUrl;
            dr["CompanyID"] = m_CompanyID;
            dr["OpenID"] = m_OpenID;
            dr["Session_Key"] = m_Session_Key;

            dr["UnionID"] = m_UnionID;
            dr["Session_True"] = m_Session_True;
            dr["Popularity"] = m_Popularity;
            dr["Thumbs"] = m_Thumbs;
            dr["QRCode"] = m_QRCode;
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
            m_UserName = dr["UserName"].ToString();
            m_UserTel = dr["UserTel"].ToString();
            m_Duty = dr["Duty"].ToString();
            m_UserEmail = dr["UserEmail"].ToString();
            m_Detail = dr["Detail"].ToString();
            m_ImgUrl = dr["ImgUrl"].ToString();
            m_CompanyID = (long)dr["CompanyID"];
            m_OpenID = dr["OpenID"].ToString();
            m_Session_Key = dr["Session_Key"].ToString();
            m_UnionID =dr["UnionID"].ToString();
            m_Session_True = dr["Session_True"].ToString();
            m_Popularity = (int)dr["Popularity"];
            m_Thumbs = (int)dr["Thumbs"];
            m_QRCode = dr["QRCode"].ToString();
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

            param = new SqlParameter("@UserName", SqlDbType.NVarChar,20);
            if (m_UserName == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_UserName.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@UserTel", SqlDbType.NVarChar, 50);
            if (m_UserTel == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_UserTel.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Duty", SqlDbType.NVarChar, 50);
            if (m_Duty == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Duty.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@UserEmail", SqlDbType.NVarChar, 50);
            if (m_UserEmail == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_UserEmail.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Detail", SqlDbType.NVarChar, 100);
            if (m_Detail == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Detail.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@ImgUrl", SqlDbType.NVarChar, 200);
            if (m_ImgUrl == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_ImgUrl.Trim();
            mCmd.Parameters.Add(param);




            param = new SqlParameter("@CompanyID", SqlDbType.BigInt);
            param.Value = m_CompanyID;
            mCmd.Parameters.Add(param);




            param = new SqlParameter("@OpenID", SqlDbType.VarChar, 100);
            if (m_OpenID == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_OpenID.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Session_Key", SqlDbType.VarChar, 1000);
            if (m_Session_Key == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Session_Key.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@UnionID", SqlDbType.VarChar, 100);
            if (m_UnionID == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_UnionID.Trim();
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Session_True", SqlDbType.VarChar, 100);
            if (m_Session_True == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_Session_True.Trim();
            mCmd.Parameters.Add(param);


            param = new SqlParameter("@Popularity", SqlDbType.Int);
            param.Value = m_Popularity;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Thumbs", SqlDbType.Int);
            param.Value = m_Thumbs;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@QRCode", SqlDbType.VarChar, 200);
            if (m_QRCode == "")
                param.Value = DBNull.Value;
            else
                param.Value = m_QRCode.Trim();
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
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get
            {
                return this.m_UserName;
            }
            set
            {
                this.m_UserName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserTel
        {
            get
            {
                return this.m_UserTel;
            }
            set
            {
                this.m_UserTel = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Duty
        {
            get
            {
                return this.m_Duty;
            }
            set
            {
                this.m_Duty = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserEmail
        {
            get
            {
                return this.m_UserEmail;
            }
            set
            {
                this.m_UserEmail = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Detail
        {
            get
            {
                return this.m_Detail;
            }
            set
            {
                this.m_Detail = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl
        {
            get
            {
                return this.m_ImgUrl;
            }
            set
            {
                this.m_ImgUrl = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CompanyID
        {
            get
            {
                return this.m_CompanyID;
            }
            set
            {
                this.m_CompanyID = value;
            }
        }





        /// <summary>
        /// 
        /// </summary>
        public string OpenID
        {
            get
            {
                return this.m_OpenID;
            }
            set
            {
                this.m_OpenID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Session_Key
        {
            get
            {
                return this.m_Session_Key;
            }
            set
            {
                this.m_Session_Key = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UnionID
        {
            get
            {
                return this.m_UnionID;
            }
            set
            {
                this.m_UnionID = value;
            }
        }
        /// <summary>
        /// session
        /// </summary>
        public string Session_True
        {
            get
            {
                return this.m_Session_True;
            }
            set
            {
                this.m_Session_True = value;
            }
        }
        /// <summary>
        ///  人气
        /// </summary>
        public int Popularity
        {
            get
            {
                return this.m_Popularity;
            }
            set
            {
                this.m_Popularity = value;
            }
        }

        /// <summary>
        ///  点赞数
        /// </summary>
        public int Thumbs
        {
            get
            {
                return this.m_Thumbs;
            }
            set
            {
                this.m_Thumbs = value;
            }
        }

        public string QRCode
        {
            get
            {
                return this.m_QRCode;
            }
            set
            {
                this.m_QRCode = value;
            }
        }

        public string CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                this.m_CreateTime = value;
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
