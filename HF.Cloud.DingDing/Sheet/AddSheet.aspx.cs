using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HF.Cloud.BLL;
using System.Data;
using HF.Cloud.DingDing.auth;

namespace HF.Cloud.DingDing.Sheet
{
    public partial class AddSheet : Base.BasePage
    {
        public string configStr = "";//config配置
        protected void Page_Load(object sender, EventArgs e)
        {
            HF.Cloud.BLL.Common.Logger.Error("addSheet001----------------");
            HF.Cloud.BLL.Common.Logger.Error("Request.Url.ToString():"+ Request.Url.ToString());
            configStr = AuthHelper.getConfig(Request.Url.ToString());//url传入当前的url
            hf_config.Value = configStr;
            if (!IsPostBack)
            {
                
                HF.Cloud.BLL.Common.Logger.Error("addSheet页面configStr:" + configStr);
                hf_mainID.Value = MainID.ToString();
                hf_WriteID.Value = UserID.ToString();
                hf_dd_userid.Value = dd_Userid;//钉钉的userid
                ClientBL Clientbll = new ClientBL();
                //绑定客户名称
                DataTable dt_Client = new DataTable();
                dt_Client = Clientbll.GetQuyClientByMainIDAndQuy(MainID, "");
                ddlCustomer.DataSource = dt_Client;
                ddlCustomer.DataTextField = "ClientName";
                ddlCustomer.DataValueField = "ID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--请选择--", "-1"));


                //绑定工单类型
                SheetBL bl = new SheetBL();
                DataTable dt_SheetType = new DataTable();
                dt_SheetType = bl.GetSheetTypeByMainID(MainID);
                ddlSheetType.DataSource = dt_SheetType;
                ddlSheetType.DataTextField = "TypeName";
                ddlSheetType.DataValueField = "ID";
                ddlSheetType.DataBind();
                ddlSheetType.Items.Insert(0, new ListItem("--请选择--", "-1"));


                //绑定服务商的服务组（比如技术组，商务组）
                UserBLL userbll = new UserBLL();
                DataTable dt_TeamList = new DataTable();
                dt_TeamList = userbll.GetTeamListBYMainID(MainID);
                ddlTeam.DataSource = dt_TeamList;
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataValueField = "ID";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("--请选择--", "-1"));

            }
        }

        protected void DdlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定受理人（先选择服务组才可以选择受理人）
            UserBLL userbll = new UserBLL();
            DataTable dt_User = new DataTable();
            string teamIDStr = ddlTeam.SelectedValue;
            dt_User = userbll.GetUserBYTeamID(long.Parse(teamIDStr));
            ddlUser.DataSource = dt_User;
            ddlUser.DataTextField = "UserName";
            ddlUser.DataValueField = "UserID";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("--请选择--", "-1"));
        }
    }
}