using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using System;
using System.IO;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class FileLogger : ILogger
    {
        private static string filePath = $"{AppContext.BaseDirectory}/logfile.txt";
        private static StreamWriter streamWriter = new StreamWriter(filePath, append: true);

        public void LogException(Exception ex)
        {
            streamWriter.WriteLine($"{DateTime.Now} {ex.Message}");
            streamWriter.Flush();
        }

        public void LogInformation(string message)
        {
            streamWriter.WriteLine($"{DateTime.Now} {message}");
            streamWriter.Flush();
        }
    }
}
