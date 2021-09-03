using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using System;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class ConsoleLogger : ILogger
    {
        public void LogException(Exception ex)
        {
            Console.WriteLine($"{DateTime.Now} {ex.Message}");
        }

        public void LogInformation(string message)
        {
            Console.WriteLine($"{DateTime.Now} {message}"); ;
        }
    }
}
