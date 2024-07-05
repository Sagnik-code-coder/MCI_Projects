using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_TrafficLog_Utility
{
    public class MCITrafficLogLogging
    {
        private static MCITrafficLogLogging _Current;

        private static readonly string logFilePath = "error.txt";

        public static MCITrafficLogLogging Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new MCITrafficLogLogging();
                }
                return _Current;
            }
        }

        private MCITrafficLogLogging()
        {
        }

        public void LogError(string categoryName, string errorMessage = null)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "Info";
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - {categoryName}: {errorMessage}");
            }
        }
    }
}
