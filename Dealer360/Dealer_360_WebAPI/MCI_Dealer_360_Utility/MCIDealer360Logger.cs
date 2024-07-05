using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Dealer_360_Utility
{
    public class MCIDealer360Logger
    {
        private static MCIDealer360Logger dealer360logger;
        private static readonly string logFilePath = "error.txt";

        private MCIDealer360Logger()
        {
        }

        public static MCIDealer360Logger GetMCIDealer360Logger()
        {
            if (dealer360logger == null)
                dealer360logger = new MCIDealer360Logger();
            return dealer360logger;
        }

        public void WriteLog(string functionName, string strLog, Exception ex = null)
        {
            if (ex != null)
            {
                LogError("MCIDealer360Logger", $"{functionName} - {ex.Message}");
                LogError("MCIDealer360Logger", $"{functionName} - {ex.InnerException?.ToString()}");
                LogError("MCIDealer360Logger", $"{functionName} - {ex.StackTrace}");
            }
            else
            {
                LogError("MCIDealer360Logger", $"{functionName} - {strLog}");
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
