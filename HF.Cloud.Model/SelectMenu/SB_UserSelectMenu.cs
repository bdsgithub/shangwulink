using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.Model.SelectMenu
{
    public class SB_UserSelectMenu
    {
        #region Model
        private long _id;
        private long _userid;
        private long _selectid;
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
        /// 人员编号
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 过滤器编号
        /// </summary>
        public long SelectID
        {
            set { _selectid = value; }
            get { return _selectid; }
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
