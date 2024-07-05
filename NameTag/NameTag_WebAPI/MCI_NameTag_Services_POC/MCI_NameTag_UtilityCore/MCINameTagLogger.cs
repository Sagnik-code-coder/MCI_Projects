using System;
using System.IO;

namespace MCI_NameTag_UtilityCore
{
    public class MCINameTagLogger
    {
        private static MCINameTagLogger nametaglogger;
        private static readonly string logFilePath = "error.txt";

        private MCINameTagLogger()
        {
        }

        public static MCINameTagLogger GetMCINameTagLogger()
        {
            if (nametaglogger == null)
                nametaglogger = new MCINameTagLogger();
            return nametaglogger;
        }

        public void WriteLog(string functionName, string strLog, Exception ex = null)
        {
            if (ex != null)
            {
                LogError("MCINameTagLogging", $"{functionName} - {ex.Message}");
                LogError("MCINameTagLogging", $"{functionName} - {ex.InnerException?.ToString()}");
                LogError("MCINameTagLogging", $"{functionName} - {ex.StackTrace}");
            }
            else
            {
                LogError("MCINameTagLogging", $"{functionName} - {strLog}");
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
