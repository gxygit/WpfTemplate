using NLog;
using System;
using System.Runtime.CompilerServices;

namespace ViewModel.Base
{
    public class Log
    {
        private static readonly Logger fileLogger = LogManager.GetLogger("info");
        public static void File(string str)
           => fileLogger.Info($"信息：{str}");
        public static void File(Exception ex, string str)
           => fileLogger.Info($"信息：{str}\r\n{ex.StackTrace}");

    }
}