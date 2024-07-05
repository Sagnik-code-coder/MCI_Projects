using System;
using System.ComponentModel;
using System.Data;
using System.IO;

public static class Logger
{
    public static void LogMessageToFile(string message, string logFilePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - {message}");
            }
        }
        catch (Exception ex)
        {
            // Handle or log any exception that might occur during file write
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
    public static DataTable ToDataTable<T>(this IList<T> data)
    {
        PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            table.Columns.Add(prop.Name, prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }
}

