using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.PrintClient.Extensions
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logInfo"></param>
        public static void WriteLog(string logInfo)
        {
            bool.TryParse(ConfigurationManager.AppSettings["enablelog"], out bool enableLog);
            if (enableLog)
            {
                File.AppendAllText(logFile, $"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】 {logInfo}\r\n");
            }
        }

        /// <summary>
        /// 日志保存路径
        /// </summary>
        public static string logFile = $"{AppContext.BaseDirectory}\\log.txt";
    }
}
