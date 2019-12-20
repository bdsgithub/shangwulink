using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HF.Cloud.Model
{
    /// <summary>
    /// 页面大小
    /// </summary>
    public class PagerInfo
    {
        /// <summary>
        /// 定义每页的大小
        /// </summary>
        public static readonly int PageSize = 10;

        /// <summary>
        /// 根据DataSet获取总记录数
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static int GetRecordCount(DataSet ds)
        {
            int count = 0;

            if (ds != null && ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    count = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                }
            }

            return count;
        }
    }

    /// <summary>
    /// 排序枚举
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 升序
        /// </summary>
        asc = 0,
        /// <summary>
        /// 降序
        /// </summary>
        desc = 1
    }
}
