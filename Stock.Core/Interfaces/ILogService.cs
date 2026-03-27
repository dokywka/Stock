using System;
using System.Collections.Generic;
using System.Text;
using StockApp.Core.Models;

namespace StockApp.Core.Interfaces
{
    public interface ILogService
    {
        List<LogEntry> GetLogs(string level=null);
    }
}
