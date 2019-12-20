using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System;

namespace HF.Cloud.BLL.Common
{
    public class ToJson<T>
    {
        public static string Json(T t)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            return js.Serialize(t);
        }
        public static Stream Json(DataTable dt)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(js.Serialize(list)));
        }
        public static string testqwe(DataTable dt)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }

            return js.Serialize(list);
        }
    }
}
