using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.BM.dropdownfilter
{
    public partial class dfClient : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string keyword = string.Empty;
            if (Request.Form["keyword"] != null)
            {
                keyword = Request.Form["keyword"].ToString();
            }

            BLL.ClientBL clientBLL = new BLL.ClientBL();
            string lookCustomerList = BLL.CustomerBL.GetLookCustomerIdList(ServiceId, CustomerUniqueCode);
            if (lookCustomerList.Length == 0)
            {
                lookCustomerList = "0";
            }
            string sql = "ID in (" + lookCustomerList + ") ";
            DataTable dt = clientBLL.GetClientListBYMainID(ServiceId);

            #region 拼接查询条件

            #endregion
            DataRow[] drs = dt.Select("ClientName like '%" + keyword + "%' and " + sql);
            //drs = drs.Select(sql);
            #region 根据数据源生成所需字符
            string strWrite = string.Empty;
            if (drs.Length > 0)
            {
                int count = drs.Length;
                if (count > 20)//判断最多显示20条数据
                {
                    count = 20;
                }
                for (int i = 0; i < count; i++)
                {
                    strWrite += string.Format("<span class=\"soption\" data-val=\"{0}\" >{1}</span> ", drs[i]["ID"], drs[i]["ClientName"]);
                }
                //strWrite += "默认最多显示20条数据，检索到" + drs.Length.ToString() + "条数据！";
            }
            else { strWrite += "无符合条件的记录！"; }
            Response.Write(strWrite);
            #endregion
        }
    }
}