using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.Cloud.DingDing.ashx
{
    /// <summary>
    /// saveAssetPatrol 的摘要说明
    /// </summary>
    public class saveAssetPatrol : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string AssetRecordId = context.Request.QueryString["AssetRecordId"];//
            string AssetStatus = context.Request.QueryString["AssetStatus"];//
            string OutSideStatus = context.Request.QueryString["OutSideStatus"];//
            string AssetDetail = context.Request.QueryString["AssetDetail"];//

            string Pic = context.Request.QueryString["Pic"];//
            string Base64 = context.Request.QueryString["Base64"];//
            string PagerOrderCode = context.Request.QueryString["PagerOrderCode"];//




            context.Response.Write("Hello World");
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