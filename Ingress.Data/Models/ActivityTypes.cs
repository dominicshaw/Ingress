using System.Collections.Generic;

namespace Ingress.Data.Models
{
    public static class ActivityTypes
    {
        public static ActivityType AnalystMeeting { get; } = new ActivityType(1, "Analyst Meeting");
        public static ActivityType CompanyMeeting { get; } = new ActivityType(2, "Company Meeting");
        public static ActivityType PhoneCall { get; } = new ActivityType(3, "Phone Call");
        public static ActivityType BrokerEmail { get; } = new ActivityType(4, "Email");
        public static ActivityType ModelAccess { get; } = new ActivityType(5, "Model Access");

        public static List<ActivityType> AllTypes { get; } = new List<ActivityType> {AnalystMeeting, CompanyMeeting, PhoneCall, BrokerEmail, ModelAccess};
    }
}