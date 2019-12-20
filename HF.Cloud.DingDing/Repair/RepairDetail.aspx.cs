using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HF.Cloud.DingDing.Repair
{
    public partial class RepairDetail : Base.BasePage
    {
        public string repairIDStr = string.Empty;//ID
        public string typeNameStr = string.Empty;//设备类型
        public string taskStatusStr = string.Empty;//所处阶段，处理？完成？

        public string realseNameStr = string.Empty;//工单创建人
        public string writeTimeStr = string.Empty;//创建时间
        public string useTimeStr = string.Empty;//已耗时

        
        public string linkNameStr = string.Empty;//客户联系人
        public string linkTelStr = string.Empty;//联系人电话

        public string acceptNameStr = string.Empty;//受理人
        public string taskDetailStr = string.Empty;//描述
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RepairID"]))
                {
                    string repairID = Request.QueryString["RepairID"].ToString();
                    hf_repairID.Value = repairID;
                    hf_userID.Value = UserID.ToString();
                    repairIDStr = repairID;
                    BLL.Common.Logger.Error("repairID" + repairID);
                    BLL.RepairTaskBLL repairBLL = new BLL.RepairTaskBLL();
                    List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();

                    try
                    {
                        json = repairBLL.GetTaskDetails(long.Parse(repairID));
                    }
                    catch (Exception err)
                    {
                        BLL.Common.Logger.Error("RepairDetail GetTaskDetails Error", err);
                    }
                    BLL.Common.Logger.Error("json.Count:" + json.Count);
                    if (json.Count > 0)
                    {
                        typeNameStr= json[0]["TypeName"].ToString();// BLL.Common.Logger.Error("typeNameStr" + typeNameStr);
                        taskStatusStr = json[0]["taskStatus"].ToString();// BLL.Common.Logger.Error("taskStatusStr" + taskStatusStr);

                        realseNameStr = json[0]["realseName"].ToString(); //BLL.Common.Logger.Error("realseNameStr" + realseNameStr);
                        writeTimeStr = json[0]["WriteTime"].ToString();// BLL.Common.Logger.Error("writeTimeStr" + writeTimeStr);
                        useTimeStr = json[0]["UseTime"].ToString(); //BLL.Common.Logger.Error("useTimeStr" + useTimeStr);

                        linkNameStr = json[0]["LinkName"].ToString(); BLL.Common.Logger.Error("linkNameStr" + linkNameStr);
                        linkTelStr = json[0]["LinkTel"].ToString(); BLL.Common.Logger.Error("linkTelStr" + linkTelStr);

                        acceptNameStr = json[0]["acceptName"].ToString(); BLL.Common.Logger.Error("acceptName" + acceptNameStr);
                        taskDetailStr = json[0]["TaskDetail"].ToString(); BLL.Common.Logger.Error("TaskDetail" + taskDetailStr);




                    }


                }
            }





        }
    }
}