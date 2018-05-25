using System;
using System.Runtime.Serialization;
using LiteDB;

namespace Ingress.DTOs
{
    [DataContract(Name = "A")]
    [KnownType(typeof(AnalystMeetingDTO))]
    [KnownType(typeof(BrokerEmailDTO))]
    [KnownType(typeof(CompanyMeetingDTO))]
    [KnownType(typeof(ModelAccessDTO))]
    [KnownType(typeof(PhoneCallDTO))]
    public class ActivityDTO
    {
        [BsonId(false)]
        [DataMember(Name = "aid")]
        public int ActivityID { get; set; }
        [DataMember(Name = "ia")]
        public DateTime InsertedAt { get; set; }
        [DataMember(Name = "us")]
        public string Username { get; set; }
        [DataMember(Name = "sj")]
        public string Subject { get; set; }
        [DataMember(Name = "ds")]
        public DateTime DateStart { get; set; }
        [DataMember(Name = "de")]
        public DateTime DateEnd { get; set; }
        [DataMember(Name = "bid")]
        public decimal? BrokerId { get; set; }
        [DataMember(Name = "brn")]
        public string BrokerName { get; set; }
        [DataMember(Name = "rt")]
        public int? Rating { get; set; }
        [DataMember(Name = "cm")]
        public string Comments { get; set; }
        [DataMember(Name = "pop")]
        public string PushOrPull { get; set; }
        [DataMember(Name = "rid")]
        public string RefID { get; set; }
        
        public ActivityDTO()
        {
            InsertedAt = DateTime.Now;
            Username = Environment.UserName;
            DateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);
        }
    }
}