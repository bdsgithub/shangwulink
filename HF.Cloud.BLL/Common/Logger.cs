using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using log4net;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace HF.Cloud.BLL.Common
{
    public class Logger
    {
        #region 系统事件日志

        public static void EventError(string errorinfo)
        {
            EventLog elog = new EventLog();
            elog.Source = "HFService";
            elog.WriteEntry(errorinfo, EventLogEntryType.Error, 8801);
        }
        public static void EventError(string errorinfo, Exception ex)
        {
            errorinfo = string.Format("info:{0};exinfo:{1};ex:{2}", errorinfo, ex.ToString(), ex.StackTrace);

            Error(errorinfo);
        }
        #endregion

        #region log4文件日志

        public static ILog Logwriter = log4net.LogManager.GetLogger(typeof(Logger));

        public static void Error(string message)
        {
            Logwriter.Error(message);
        }
        public static void Info(string message)
        {
            Logwriter.Info(message);
        }
        public static void Error(string message, Exception ex)
        {
            Logwriter.Error(message, ex);
        }
        #endregion
    }
}
