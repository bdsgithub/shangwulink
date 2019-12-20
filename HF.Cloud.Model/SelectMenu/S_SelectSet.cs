using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model.SelectMenu
{
    public class S_SelectSet
    {
        #region Model
        private long _id;
        private long _selectid;
        private string _name;
        private int _optype;
        private string _value1;
        private string _value2;
        private string _remark;
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
        /// 过滤器ID
        /// </summary>
        public long SelectID
        {
            set { _selectid = value; }
            get { return _selectid; }
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
        /// 类型（是|非|区间|大于|小于。。）
        /// </summary>
        public int OpType
        {
            set { _optype = value; }
            get { return _optype; }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public string Value1
        {
            set { _value1 = value; }
            get { return _value1; }
        }
        /// <summary>
        /// 值2
        /// </summary>
        public string Value2
        {
            set { _value2 = value; }
            get { return _value2; }
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
