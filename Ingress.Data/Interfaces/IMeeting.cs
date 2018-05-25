using System;

namespace Ingress.Data.Interfaces
{
    public interface IMeeting
    {
        DateTime InsertedAt { get; set; }
        string Username { get; set; }
        string Subject { get; set; }
        DateTime DateStart { get; set; }
        DateTime DateEnd { get; set; }
        decimal? BrokerId { get; set; }
        string BrokerName { get; set; }
        int? Rating { get; set; }
        string Comments { get; set; }
        string PushOrPull { get; set; }

        string CalID { get; set; }
        string Categories { get; set; }
        string ConvoID { get; set; }
        string GlobalID { get; set; }
        string Organiser { get; set; }
    }
}