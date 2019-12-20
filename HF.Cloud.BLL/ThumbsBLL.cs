using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.Model;
using System.Data;

namespace HF.Cloud.BLL
{
   public class ThumbsBLL
    {
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="session">接收点赞人的session（好友）</param>
        /// <param name="session_Thumb">点赞人的session（自己）</param>
        /// <returns></returns>
        public string AddThumb(string session, string session_Thumb)
        {
            BLL.Common.Logger.Error("AddThumb方法接受到的参数session：" + session + "---session_Thumb：" + session_Thumb);
            string resultStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            long userID_Thumb = userBLL.GetUserIDBySession(session_Thumb);
            BLL.Common.Logger.Error("AddThumb方法用户的ID userID：" + userID + "---userID_Thumb：" + userID_Thumb);
            //添加记录
            SB_ThumbsEL thumbsEl = new SB_ThumbsEL();
            thumbsEl.UserID = userID;
            thumbsEl.UserID_Thumb = userID_Thumb;
            thumbsEl.CreateTime = DateTime.Now.ToString();
            thumbsEl.Valid = 1;
            int outValue;
            long executeResul = thumbsEl.ExecNonQuery(1, out outValue);
            BLL.Common.Logger.Error("AddThumb方法点赞，数据库添加结果（大于0正确）：" + executeResul);
            if (executeResul > 0)
            {
                resultStr = "success";
            }
            else
            {
                resultStr = "error";
            }
            return resultStr;
        }


        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="session">被点赞人的session</param>
        /// <param name="session_Thumb">点赞人的session</param>
        /// <returns></returns>
        public string CancelThumb(string session, string session_Thumb)
        {
            BLL.Common.Logger.Error("CancelThumb方法接受到的参数session：" + session + "---session_Thumb：" + session_Thumb);
            string resultStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            long userID_Thumb = userBLL.GetUserIDBySession(session_Thumb);
            BLL.Common.Logger.Error("CancelThumb方法用户的ID userID：" + userID + "---userID_Thumb：" + userID_Thumb);
            SB_ThumbsEL thumbsEl = new SB_ThumbsEL();
            thumbsEl.UserID = userID;
            thumbsEl.UserID_Thumb = userID_Thumb;
            int outValue;
            long executeResul = thumbsEl.ExecNonQuery(2, out outValue);
            BLL.Common.Logger.Error("CancelThumb方法取消点赞，数据库修改结果（大于0正确）：" + outValue);
            if (outValue > 0)
            {
                resultStr = "success";
            }
            else
            {
                resultStr = "error";
            }
            return resultStr;

        }


        /// <summary>
        /// 获取点赞个数
        /// </summary>
        /// <param name="session">要查询的用户session</param>
        /// <returns></returns>
        public string GetThumbs(string session)
        {
            string returnStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            long userID = userBLL.GetUserIDBySession(session);
            SB_ThumbsEL thumbsEl = new SB_ThumbsEL();
            thumbsEl.UserID = userID;
            DataTable dataT = thumbsEl.ExecDT(3);
            if(dataT != null && dataT.Rows.Count > 0)
            {
                returnStr= dataT.Rows.Count.ToString();
            }
            else
            {
                returnStr = "0";
            }
            return returnStr;
        }


        /// <summary>
        /// 是否点过赞
        /// </summary>
        /// <param name="session">被查看人session</param>
        /// <param name="session_Thumb">本人session</param>
        /// <returns></returns>
        public string IsHadThumb(string session, string session_Thumb)
        {
            string resultStr = "";
            //通过Session换取UserID
            UserBLL userBLL = new UserBLL();
            //session_Thumb点赞表中这个是主题,注意！！！这里要搞清关系
            long userID = userBLL.GetUserIDBySession(session);
            //操作人的ID（自己的ID）
            long userID_Thumb = userBLL.GetUserIDBySession(session_Thumb);
            //点赞表中查看记录
            SB_ThumbsEL thumbsEL = new SB_ThumbsEL();
            thumbsEL.UserID = userID;
            thumbsEL.UserID_Thumb = userID_Thumb;
            DataTable dt = thumbsEL.ExecDT(31);
            if(dt!=null&&dt.Rows.Count>0)
            {
                resultStr = "1";
            }
            else
            {
                resultStr = "0";
            }
            return resultStr;
        }









    }
}
