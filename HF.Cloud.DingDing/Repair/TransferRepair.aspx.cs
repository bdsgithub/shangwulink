using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using HF.Cloud.BLL;
using System.Data;

namespace HF.Cloud.DingDing.Repair
{
    public partial class TransferRepair :Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(!String.IsNullOrEmpty(Request.QueryString["RepairID"]))
                {
                    string repairID = Request.QueryString["RepairID"].ToString();
                    hf_repairID.Value = repairID;
                    hf_userID.Value = UserID.ToString();
                }
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