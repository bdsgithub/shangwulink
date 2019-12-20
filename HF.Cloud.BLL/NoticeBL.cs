using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HF.Cloud.BLL
{
    public class NoticeBL
    {
        /// <summary>
        /// 表格无数据的提示
        /// </summary>
        /// <returns></returns>
        public static string GetEmptyTableTips()
        {
            return "<div style='text-align:center;height:50px;line-height:50px;'>暂无数据</div>";
        }
    }
}
