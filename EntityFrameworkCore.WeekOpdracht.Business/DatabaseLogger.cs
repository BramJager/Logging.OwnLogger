using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using System;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.WeekOpdracht.Business
{
    public class DatabaseLogger: Interfaces.ILogger
    {
        private readonly DataContext dataContext;

        public DatabaseLogger(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void LogException(Exception ex)
        {
            Log newLog = new Log() {Message = ex.Message, Date = DateTime.Now, Level = LogLevel.Error.ToString() };
            dataContext.Add(newLog);
            dataContext.SaveChanges();
        }

        public void LogInformation(string message)
        {
            Log newLog = new Log() { Message = message, Date = DateTime.Now, Level = LogLevel.Information.ToString() };
            dataContext.Add(newLog);
            dataContext.SaveChanges();
        }
    }
}
