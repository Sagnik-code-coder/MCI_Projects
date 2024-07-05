using System;
using System.Collections.Generic;
using System.IO;
//using Microsoft.SharePoint.Client;

namespace MCI_NameTag_UtilityCore
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