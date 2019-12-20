using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.Model;
using System.Data;

namespace HF.Cloud.BLL
{
  public  class PopularityBLL
    {
        /// <summary>
        /// 添加查看（人气）记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userID_look">查看人用户ID</param>
        /// <returns></returns>
        public long  InsertPopularity(long userID,long userID_look)
        {
            SB_PopularityEL popularityEL = new SB_PopularityEL();
            popularityEL.UserID = userID;
            popularityEL.UserID_Look = userID_look;
            popularityEL.CreateTime = DateTime.Now.ToString();
            popularityEL.Valid = 1;
            int ra;
            long execValue = popularityEL.ExecNonQuery(1, out ra);
            return execValue;
        }


        /// <summary>
        /// 查看是否查看过
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userID_look">查看人用户ID</param>
        /// <returns></returns>
        public bool IsLooked(long userID, long userID_Look)
        {
            SB_PopularityEL popularityEL = new SB_PopularityEL();
            popularityEL.UserID = userID;
            popularityEL.UserID_Look = userID_Look;
           
            DataTable  dt = popularityEL.ExecDT(3);
            if(dt!=null&&dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获取被查看数（人气值）
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public int GetPopularityNumber(long userID)
        {
            int reInt = 0;
            SB_PopularityEL popularityEL = new SB_PopularityEL();
            popularityEL.UserID = userID;//只需要UserID就可以，用户有几个记录就被查看过几次
            DataTable dt = popularityEL.ExecDT(31);
            if (dt != null && dt.Rows.Count > 0)
            {
                reInt = dt.Rows.Count;
            }
            return reInt;
        }






    }
}
