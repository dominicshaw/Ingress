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
        private readonly Dictionary<int, Func<ActivityViewModel>> _creator = new Dictionary<int, Func<ActivityViewModel>>();

        public event NewActivityEventHandler NewActivity;

        public NewActivityFactory(ILog log, IActivityRepository repository)
        {
            _log = log;

            _creator.Add(ActivityTypes.AnalystMeeting.ID, () => new AnalystMeetingViewModel(repository, new AnalystMeeting()));
            _creator.Add(ActivityTypes.CompanyMeeting.ID, () => new CompanyMeetingViewModel(repository, new CompanyMeeting()));
            _creator.Add(ActivityTypes.PhoneCall.ID, () => new PhoneCallViewModel(repository, new PhoneCall()));
            _creator.Add(ActivityTypes.BrokerEmail.ID, () => new BrokerEmailViewModel(repository, new BrokerEmail()));
            _creator.Add(ActivityTypes.ModelAccess.ID, () => new ModelAccessViewModel(repository, new ModelAccess()));

            Messenger.Default.Register<NewActivityRequest>(this, MessengerMessageReceived);
        }

        private void MessengerMessageReceived(NewActivityRequest message)
        {
            _log.Info("New Activity: " + message.ActivityType);
            NewActivity?.Invoke(_creator[message.ActivityType.ID].Invoke());
        }
    }
}