using NLog;
using System;
using System.Runtime.CompilerServices;

namespace WpfTemplate
{
    /// <summary>
    /// 
    /// </summary>
    public class Log
    {
        private static Logger fileLogger = LogManager.GetLogger("info");

#if DEBUG
        public static void File(string str,
           [CallerMemberName]string origin = "",
           [CallerFilePath]string filePath = "",
           [CallerLineNumber]int lineNumber = 0)
           => fileLogger.Info($"文件{filePath},行号{lineNumber}{Environment.NewLine}\t\t\t 信息：{str}");
#else
         public static void File(string str,
            [CallerMemberName]string origin = "",
            [CallerFilePath]string filePath = "",
            [CallerLineNumber]int lineNumber = 0)
            => fileLogger.Info(str);
#endif

    }
}