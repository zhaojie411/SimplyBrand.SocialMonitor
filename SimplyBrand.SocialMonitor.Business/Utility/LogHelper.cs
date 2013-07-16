using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{

    /// <summary>
    /// 日志记录
    /// </summary>
    public class LogHelper
    {
        private LogHelper()
        {
            SetConfig();
        }

        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        private static bool IsLoadConfig = false;
        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(string info, Exception se)
        {
            if (!IsLoadConfig)
            {
                SetConfig();
                IsLoadConfig = true;
            }
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog("", ex);
        }
    }

}
