using System;

namespace Ingress.Data.Models
{
    public abstract class Activity
    {
        public int ActivityID { get; set; }
        public DateTime InsertedAt { get; set; }
        public string Username { get; set; }
        public string Subject { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal? BrokerId { get; set; }
        public string BrokerName { get; set; }
        public int? Rating { get; set; }
        public string Comments { get; set; }
        public string PushOrPull { get; set; }
    }
}
