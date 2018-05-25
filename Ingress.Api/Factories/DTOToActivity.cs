using System;
using System.Collections.Generic;
using Ingress.Data.Models;
using Ingress.DTOs;

namespace Ingress.Api.Factories
{
    public static class DTOToActivity
    {
        private static readonly Dictionary<Type, Func<ActivityDTO, Activity>> _fromDTO = new Dictionary<Type, Func<ActivityDTO, Activity>>();

        static DTOToActivity()
        {
            _fromDTO.Add(typeof(AnalystMeetingDTO), activity => Create((AnalystMeetingDTO) activity));
            _fromDTO.Add(typeof(CompanyMeetingDTO), activity => Create((CompanyMeetingDTO) activity));
            _fromDTO.Add(typeof(PhoneCallDTO),      activity => Create((PhoneCallDTO)      activity));
            _fromDTO.Add(typeof(BrokerEmailDTO),    activity => Create((BrokerEmailDTO)    activity));
            _fromDTO.Add(typeof(ModelAccessDTO),    activity => Create((ModelAccessDTO)    activity));
        }

        private static AnalystMeeting Create(AnalystMeetingDTO dto)
        {
            var activity = new AnalystMeeting();

            Load(activity, dto);

            activity.Analyst      = dto.Analyst;
            activity.CalID        = dto.CalID;
            activity.Categories   = dto.Categories;
            activity.ConvoID      = dto.ConvoID;
            activity.GlobalID     = dto.GlobalID;
            activity.IsConference = dto.IsConference;
            activity.Organiser    = dto.Organiser;
            activity.Skipped      = dto.Skipped;
            activity.TimeTaken    = dto.TimeTaken;

            return activity;
        }

        private static CompanyMeeting Create(CompanyMeetingDTO dto)
        {
            var activity = new CompanyMeeting();

            Load(activity, dto);

            activity.CalID      = dto.CalID;
            activity.Categories = dto.Categories;
            activity.ConvoID    = dto.ConvoID;
            activity.GlobalID   = dto.GlobalID;
            activity.IsDirect   = dto.IsDirect;
            activity.Organiser  = dto.Organiser;
            activity.Skipped    = dto.Skipped;

            return activity;
        }

        private static PhoneCall Create(PhoneCallDTO dto)
        {
            var activity = new PhoneCall();

            Load(activity, dto);

            activity.Analyst = dto.Analyst;
            activity.TimeTaken = dto.TimeTaken;

            return activity;
        }

        private static BrokerEmail Create(BrokerEmailDTO dto)
        {
            var activity = new BrokerEmail();

            Load(activity, dto);

            activity.Analyst = dto.Analyst;

            return activity;
        }

        private static ModelAccess Create(ModelAccessDTO dto)
        {
            var activity = new ModelAccess();

            Load(activity, dto);

            activity.Analyst = dto.Analyst;

            return activity;
        }

        private static void Load(Activity activity, ActivityDTO dto)
        {
            activity.ActivityID = dto.ActivityID;
            activity.Subject    = dto.Subject;
            activity.Username   = dto.Username;
            activity.DateStart  = dto.DateStart;
            activity.DateEnd    = dto.DateEnd;
            activity.BrokerId   = dto.BrokerId;
            activity.BrokerName = dto.BrokerName;
            activity.Comments   = dto.Comments;
            activity.InsertedAt = dto.InsertedAt;
            activity.PushOrPull = dto.PushOrPull;
            activity.Rating     = dto.Rating;
            activity.RefID      = dto.RefID;
        }

        public static Activity Get(ActivityDTO dto)
        {
            return _fromDTO[dto.GetType()].Invoke(dto);
        }
    }
}