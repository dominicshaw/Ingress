using System;
using System.Collections.Generic;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using Ingress.WPF.ViewModels.Data;
using JetBrains.Annotations;

namespace Ingress.WPF.Factories
{
    [UsedImplicitly]
    public class LoadActivityFactory : ILoadActivityFactory
    {
        private readonly Dictionary<Type, Func<Activity, ActivityViewModel>> _loadActivities = new Dictionary<Type, Func<Activity, ActivityViewModel>>();

        public LoadActivityFactory(IActivityRepository repository)
        {
            _loadActivities.Add(typeof(AnalystMeeting), activity => new AnalystMeetingViewModel((AnalystMeeting) activity));
            _loadActivities.Add(typeof(CompanyMeeting), activity => new CompanyMeetingViewModel((CompanyMeeting) activity));
            _loadActivities.Add(typeof(PhoneCall)     , activity => new PhoneCallViewModel     ((PhoneCall)      activity));
            _loadActivities.Add(typeof(BrokerEmail)   , activity => new BrokerEmailViewModel   ((BrokerEmail)    activity));
            _loadActivities.Add(typeof(ModelAccess)   , activity => new ModelAccessViewModel   ((ModelAccess)    activity));
        }

        public ActivityViewModel Load(Activity activity)
        {
            return _loadActivities[activity.GetType()].Invoke(activity);
        }
    }
}