namespace MCI_TrafficLog_Utility
{
    public class AppSettings
    {
        private static readonly string logFilePath = "error.txt";



        private static void WriteLog(string functionName, string loggerName, Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - Function: {functionName}, Logger: {loggerName}, Error: {ex?.Message}");
            }
        }
    }
}