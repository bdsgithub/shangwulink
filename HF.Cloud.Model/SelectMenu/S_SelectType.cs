using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model.SelectMenu
{
    public class S_SelectType
    {
        #region Model
        private long _id;
        private long _mainid;
        private string _name;
        private int _code;
        private string _remark;
        private bool _status;
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
        /// 类型名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 类型代码
        /// </summary>
        public int Code
        {
            set { _code = value; }
            get { return _code; }
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
        /// 是否有效
        /// </summary>
        public bool Valid
        {
            set { _valid = value; }
            get { return _valid; }
        }
        #endregion Model
    }
}
