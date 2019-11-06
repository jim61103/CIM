using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class LogHelper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LogHelper));

        public static void Error(Exception ex)
        {
            logger.Error(ex);
        }

        public static void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Debug(Exception ex)
        {
            logger.Debug(ex);
        }

        public static void Warn(Exception ex)
        {
            logger.Warn(ex);
        }

        /// 
        /// 初始化 log4net 設定檔位置
        /// 
        /// firePath 例如：d:/config.xml
        /// 
        public static void InitConfig(string filePath)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo(filePath));
        }
    }
}
