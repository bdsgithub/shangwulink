using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HF.Cloud.BLL;

namespace HF.Cloud.DingDing.Patrol
{
    public partial class PatrolDetail : Base.BasePage
    {
        public string assetName = string.Empty;// 设备名称
        public string brandName = string.Empty;// 品牌
        public string modelName = string.Empty;// 型号
        public string assetXuLie = string.Empty;// 序列号

        public string assetStatus = string.Empty;// 状态
        public string outSideStatus = string.Empty;// 外观
        public string pagerOrderCode = string.Empty;// 纸质巡检标号
        public string assetDetail = string.Empty;// 巡检描述


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
                string assetRecordId = string.Empty;// 巡检记录ID
                if (!string.IsNullOrEmpty(Request.QueryString["assetRecordId"]))
                {
                    assetRecordId = Request.QueryString["assetRecordId"].ToString();
                }
                hf_AssetRecordId.Value = assetRecordId;
                //获取巡检记录信息
                BLL.InspectBL inspectBLL = new InspectBL();
                DataTable dt = new DataTable();
                try
                {
                    dt = inspectBLL.LookInspectAsset(long.Parse(assetRecordId));
                }
                catch (Exception err)
                {
                    HF.Cloud.BLL.Common.Logger.Error("PatrolDetail.aspx Error", err);
                }
                HF.Cloud.BLL.Common.Logger.Error("patrolDetail信息个数:" + dt.Rows.Count.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    assetName = dt.Rows[0]["AssetName"].ToString();
                    brandName = dt.Rows[0]["BrandName"].ToString();
                    modelName = dt.Rows[0]["ModelName"].ToString();
                    assetXuLie = dt.Rows[0]["AssetXuLie"].ToString();

                    assetStatus = dt.Rows[0]["AssetStatus"].ToString();
                    outSideStatus = dt.Rows[0]["OutSideStatus"].ToString();
                    pagerOrderCode = dt.Rows[0]["PagerOrderCode"].ToString();
                    assetDetail = dt.Rows[0]["AssetDetail"].ToString();


                    if(assetStatus=="1")
                    {
                        sate01.Checked = true;
                    }
                    else
                    {
                        sate02.Checked = true;
                    }
                    if (outSideStatus == "1")
                    {
                        outside01.Checked = true;
                    }
                    else
                    {
                        outside02.Checked = true;
                    }

                    txtPagerOrderCode.Value = pagerOrderCode;
                    assetDetail.Insert(0, assetDetail);

                }


            }
        }
    }
}