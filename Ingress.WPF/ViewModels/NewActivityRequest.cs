using Ingress.Data.Models;

namespace Ingress.WPF.ViewModels
{
    public class NewActivityRequest
    {
        public ActivityType ActivityType { get; }

        public NewActivityRequest(ActivityType activityType)
        {
            ActivityType = activityType;
        }
    }
}