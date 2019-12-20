using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.Cloud.BM.ashx
{
    /// <summary>
    /// ExecuteQBCode 的摘要说明
    /// </summary>
    public class ExecuteQBCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Form["op"] != null)
            {
                string returnHtml = string.Empty;
                string op = context.Request.Form["op"];
                switch (op)
                {
                    case "deleteModel"://删除标签模板
                        {
                            Model.QB_ModelEL model = new Model.QB_ModelEL();
                            model.ID = long.Parse(context.Request.Form["id"]);
                            model.ExecNonQuery(3);
                            returnHtml = "success";
                        }
                        break;
                }
                context.Response.Write(returnHtml);
            }
            else
            {
                #region 修改标签的打印次数
                string qbCodeList = GetQBCode(context);
                long mainId = GetMainId(context);

                if (qbCodeList != string.Empty)
                {
                    try
                    {
                        if (qbCodeList.Substring(qbCodeList.Length - 1) == ",")
                        {
                            qbCodeList = qbCodeList.Remove(qbCodeList.Length - 1);
                        }

                        string[] codeList = qbCodeList.Split(new char[] { ',' });

                        Model.QB_CodeEL qbCode = new Model.QB_CodeEL();

                        foreach (string code in codeList)
                        {
                            qbCode.QBCode = code;
                            qbCode.MainID = mainId;
                            qbCode.ExecuteEL(55);

                            qbCode.PrintNum = qbCode.PrintNum + 1;

                            qbCode.ExecNonQuery(2);
                        }
                    }
                    catch { }
                }
                #endregion
            }
        }


        /// <summary>
        /// 获得传入的标签编号
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string GetQBCode(HttpContext context)
        {
            string code = string.Empty;

            if (!string.IsNullOrEmpty(context.Request["cl"]))
            {
                code = context.Request.Form["cl"].Trim();
            }

            return code;
        }

        protected long GetMainId(HttpContext context)
        {
            long code = 0;

            if (!string.IsNullOrEmpty(context.Request["mid"]))
            {
                code = long.Parse(context.Request["mid"].Trim());
            }

            return code;
        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}