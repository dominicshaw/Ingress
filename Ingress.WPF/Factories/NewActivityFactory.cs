using System;
using System.Collections.Generic;
using DevExpress.Mvvm;
using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using Ingress.WPF.ViewModels;
using Ingress.WPF.ViewModels.Data;
using JetBrains.Annotations;
using log4net;

namespace Ingress.WPF.Factories
{
    [UsedImplicitly]
    public class NewActivityFactory : INewActivityFactory
    {
        private readonly ILog _log;
        private readonly Dictionary<int, Func<ActivityViewModel>> _newActivities = new Dictionary<int, Func<ActivityViewModel>>();
        private readonly Dictionary<Type, Func<Activity, ActivityViewModel>> _loadActivities = new Dictionary<Type, Func<Activity, ActivityViewModel>>();

        public event NewActivityEventHandler NewActivity;

        public NewActivityFactory(ILog log, IActivityRepository repository)
        {
            _log = log;

            _newActivities.Add(ActivityTypes.AnalystMeeting.ID, () => new AnalystMeetingViewModel(new AnalystMeeting()));
            _newActivities.Add(ActivityTypes.CompanyMeeting.ID, () => new CompanyMeetingViewModel(new CompanyMeeting()));
            _newActivities.Add(ActivityTypes.PhoneCall.ID, () => new PhoneCallViewModel(new PhoneCall()));
            _newActivities.Add(ActivityTypes.BrokerEmail.ID, () => new BrokerEmailViewModel(new BrokerEmail()));
            _newActivities.Add(ActivityTypes.ModelAccess.ID, () => new ModelAccessViewModel(new ModelAccess()));

            _loadActivities.Add(typeof(AnalystMeeting), activity => new AnalystMeetingViewModel((AnalystMeeting) activity));
            _loadActivities.Add(typeof(CompanyMeeting), activity => new CompanyMeetingViewModel((CompanyMeeting) activity));
            _loadActivities.Add(typeof(PhoneCall), activity => new PhoneCallViewModel((PhoneCall) activity));
            _loadActivities.Add(typeof(BrokerEmail), activity => new BrokerEmailViewModel((BrokerEmail) activity));
            _loadActivities.Add(typeof(ModelAccess), activity => new ModelAccessViewModel((ModelAccess) activity));

            Messenger.Default.Register<NewActivityRequest>(this, MessengerMessageReceived);
        }

        public ActivityViewModel Load(Activity activity)
        {
            return _loadActivities[activity.GetType()].Invoke(activity);
        }

        private void MessengerMessageReceived(NewActivityRequest message)
        {
            _log.Info("New Activity: " + message.ActivityType);
            NewActivity?.Invoke(_newActivities[message.ActivityType.ID].Invoke());
        }
    }
}