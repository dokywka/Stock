using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockApp.Core.Models
{
    [BsonIgnoreExtraElements]
    public class LogEntry
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] // позволяет использовать string вместо ObjectId
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        [BsonElement("Level")]
        public string Level {  get; set; }
        [BsonElement("RenderedMessage")]
        public string Message {  get; set; }
        public string Exception { get; set; }
    }
}
