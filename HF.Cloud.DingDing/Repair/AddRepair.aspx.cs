using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HF.Cloud.BLL;
using System.Data;
using HF.Cloud.DingDing.auth;
using Newtonsoft.Json.Linq;

namespace HF.Cloud.DingDing.Repair
{
    public partial class AddRepair : Base.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                hf_mainID.Value = MainID.ToString();
                hf_WriteID.Value = UserID.ToString();
                //绑定设备类型
                BLL.AssetBL assetBLL = new AssetBL();
                string assetType = string.Empty;
                try
                {
                    assetType = assetBLL.GetAssetType(MainID);
                }
                catch (Exception err)
                {
                    BLL.Common.Logger.Error("AddRepair Error", err);
                }

                JArray arryassetType = JArray.Parse(assetType);
                for (int i = 0; i < arryassetType.Count; i++)
                {
                    JObject objType = JObject.Parse(arryassetType[i].ToString());
                    ddlAssetType.Items.Add(new ListItem(objType["TypeName"].ToString(), objType["ID"].ToString()));

                }
                ddlAssetType.Items.Insert(0, new ListItem("--请选择--", "-1"));


                //绑定客户名称
                ClientBL Clientbll = new ClientBL();
                DataTable dt_Client = new DataTable();
                dt_Client = Clientbll.GetQuyClientByMainIDAndQuy(MainID, "");
                ddlCustomer.DataSource = dt_Client;
                ddlCustomer.DataTextField = "ClientName";
                ddlCustomer.DataValueField = "ID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, new ListItem("--请选择--", "-1"));




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