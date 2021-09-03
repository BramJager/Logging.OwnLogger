using System;

namespace EntityFrameworkCore.WeekOpdracht.Business.Interfaces
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogException(Exception ex);
    }
}
