using StockApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using MongoDB.Driver;
using StockApp.Core.Models;

namespace StockApp.Infrastructure
{
    public class LogService:ILogService
    {
        private readonly IMongoCollection<LogEntry> _logs;
        public LogService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("StockAppLogs");
            _logs = db.GetCollection<LogEntry>("logs");
        }
        public List<LogEntry> GetLogs(string level = null)
        {
            var filter = level == null
                ? Builders<LogEntry>.Filter.Empty
                : Builders<LogEntry>.Filter.Eq(x => x.Level, level);//так в mongodb делается 

            return _logs.Find(filter)
                        .SortByDescending(x => x.Timestamp)
                        .Limit(100)
                        .ToList();
        }
    }
}
