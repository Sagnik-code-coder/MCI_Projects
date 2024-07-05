using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_TrafficLog_Utility
{
    public class MCITrafficLogLogger
    {
        private static MCITrafficLogLogger nametaglogger;
        private static readonly string logFilePath = "error.txt";

        private MCITrafficLogLogger()
        {
        }

        public static MCITrafficLogLogger GetMCITrafficLogLogger()
        {
            if (nametaglogger == null)
                nametaglogger = new MCITrafficLogLogger();
            return nametaglogger;
        }

        public void WriteLog(string functionName, string strLog, Exception ex = null)
        {
            if (ex != null)
            {
                LogError("MCITrafficLogLogger", $"{functionName} - {ex.Message}");
                LogError("MCITrafficLogLogger", $"{functionName} - {ex.InnerException?.ToString()}");
                LogError("MCITrafficLogLogger", $"{functionName} - {ex.StackTrace}");
            }
            else
            {
                LogError("MCITrafficLogLogger", $"{functionName} - {strLog}");
            }
        }

        private void LogError(string logListTitle, string logMessage)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - {logListTitle}: {logMessage}");
            }
        }
    }
}
