using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_Dealer_360_Utility
{
    public class MCIDealer360Logging
    {
        private static MCIDealer360Logging _Current;

        private static readonly string logFilePath = "error.txt";

        public static MCIDealer360Logging Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new MCIDealer360Logging();
                }
                return _Current;
            }
        }

        private MCIDealer360Logging()
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
