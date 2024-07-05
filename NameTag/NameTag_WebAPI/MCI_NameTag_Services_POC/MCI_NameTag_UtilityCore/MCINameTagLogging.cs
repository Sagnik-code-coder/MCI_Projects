using System;
using System.IO;

namespace MCI_NameTag_UtilityCore
{
    public class MCINameTagLogging
    {
        private static MCINameTagLogging _Current;

        private static readonly string logFilePath = "error.txt";

        public static MCINameTagLogging Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new MCINameTagLogging();
                }
                return _Current;
            }
        }

        private MCINameTagLogging()
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
