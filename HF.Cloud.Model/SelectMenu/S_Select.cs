using HF.Cloud.CommonDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model.SelectMenu
{
    public class S_Select
    {
        #region Model
        private long _id;
        private long _mainid;
        private string _name = "";
        private long _selecttype;
        private string _selectwhere = "";
        private string _remark = "";
        private bool _status;
        private int _sortno;
        private int _readrange;
        private bool _valid;
        /// <summary>
        /// 编号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 服务商ID
        /// </summary>
        public long MainID
        {
            set { _mainid = value; }
            get { return _mainid; }
        }
        /// <summary>
        /// 过滤器名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 所属分类（1工单2客户3设备）
        /// </summary>
        public long SelectType
        {
            set { _selecttype = value; }
            get { return _selecttype; }
        }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string SelectWhere
        {
            set { _selectwhere = value; }
            get { return _selectwhere; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 状态（1启用0禁用）
        /// </summary>
        public bool Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 访问范围（1全部访问2组访问3指定人员）
        /// </summary>
        public int ReadRange
        {
            set { _readrange = value; }
            get { return _readrange; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Valid
        {
            set { _valid = value; }
            get { return _valid; }
        }
        #endregion Model

        #region dal

        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string proceName = "sp_S_Select";

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

        public long ExecNonQuery(int iOperateFlag)
        {
            SqlParameterCollection sqlpc = InitCmd(iOperateFlag);
            int rowsAffected = 0;
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
        /// ExecuteEL
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

        /// <summary>
        /// 实体转换表
        /// </summary>
        /// <returns>由实体所填充的表，最多有一行数据</returns>
        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("MainID", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("SelectType", typeof(long));
            dt.Columns.Add("SelectWhere", typeof(string));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("Status", typeof(bool));
            dt.Columns.Add("SortNo", typeof(int));
            dt.Columns.Add("ReadRange", typeof(int));
            dt.Columns.Add("Valid", typeof(bool));

            DataRow dr = dt.NewRow();
            dr["ID"] = ID;
            dr["MainID"] = MainID;
            dr["Name"] = Name;
            dr["SelectType"] = SelectType;
            dr["SelectWhere"] = SelectWhere;
            dr["Remark"] = Remark;
            dr["Status"] = Status;
            dr["SortNo"] = SortNo;
            dr["ReadRange"] = ReadRange;

            dr["Valid"] = Valid;
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
            MainID = (long)dr["MainID"];
            Name = (string)dr["Name"];
            SelectType = (long)dr["SelectType"];
            SelectWhere = (string)dr["SelectWhere"];
            Remark = dr["Remark"].ToString();
            Status = (bool)dr["Status"];
            SortNo = (int)dr["SortNo"];
            ReadRange = (int)dr["ReadRange"];

            Valid = (bool)dr["Valid"];
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

            param = new SqlParameter("@MainID", SqlDbType.BigInt);
            param.Value = MainID;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Name", SqlDbType.NVarChar);
            param.Value = Name;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@SelectWhere", SqlDbType.VarChar);
            param.Value = SelectWhere;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@SelectType", SqlDbType.BigInt);
            param.Value = SelectType;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Remark", SqlDbType.NVarChar);
            param.Value = Remark;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Status", SqlDbType.Bit);
            param.Value = Status;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@SortNo", SqlDbType.Int);
            param.Value = SortNo;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@ReadRange", SqlDbType.Int);
            param.Value = ReadRange;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@Valid", SqlDbType.Bit);
            param.Value = Valid;
            mCmd.Parameters.Add(param);

            param = new SqlParameter("@OperateFlag", SqlDbType.Int);
            param.Value = iOperateFlag;
            mCmd.Parameters.Add(param);

            return mCmd.Parameters;
        }

        #endregion
    }
}
