using System;
using System.Collections.Generic;
using Ingress.Data.Models;
using Ingress.DTOs;

namespace Ingress.Api.Factories
{
    public static class ActivityToDTO
    {
        private static readonly Dictionary<Type, Func<Activity, ActivityDTO>> _toDTO = new Dictionary<Type, Func<Activity, ActivityDTO>>();

        static ActivityToDTO()
        {
            _toDTO.Add(typeof(AnalystMeeting), activity => Create((AnalystMeeting) activity));
            _toDTO.Add(typeof(CompanyMeeting), activity => Create((CompanyMeeting) activity));
            _toDTO.Add(typeof(PhoneCall),      activity => Create((PhoneCall)      activity));
            _toDTO.Add(typeof(BrokerEmail),    activity => Create((BrokerEmail)    activity));
            _toDTO.Add(typeof(ModelAccess),    activity => Create((ModelAccess)    activity));
        }

        private static AnalystMeetingDTO Create(AnalystMeeting activity)
        {
            var dto = new AnalystMeetingDTO();

            Load(dto, activity);

            dto.Analyst      = activity.Analyst;
            dto.CalID        = activity.CalID;
            dto.Categories   = activity.Categories;
            dto.ConvoID      = activity.ConvoID;
            dto.GlobalID     = activity.GlobalID;
            dto.IsConference = activity.IsConference;
            dto.Organiser    = activity.Organiser;
            dto.Skipped      = activity.Skipped;
            dto.TimeTaken    = activity.TimeTaken;

            return dto;
        }

        private static CompanyMeetingDTO Create(CompanyMeeting activity)
        {
            var dto = new CompanyMeetingDTO();

            Load(dto, activity);

            dto.CalID        = activity.CalID;
            dto.Categories   = activity.Categories;
            dto.ConvoID      = activity.ConvoID;
            dto.GlobalID     = activity.GlobalID;
            dto.IsDirect     = activity.IsDirect;
            dto.Organiser    = activity.Organiser;
            dto.Skipped      = activity.Skipped;

            return dto;
        }

        private static PhoneCallDTO Create(PhoneCall activity)
        {
            var dto = new PhoneCallDTO();

            Load(dto, activity);

            dto.Analyst   = activity.Analyst;
            dto.TimeTaken = activity.TimeTaken;

            return dto;
        }

        private static BrokerEmailDTO Create(BrokerEmail activity)
        {
            var dto = new BrokerEmailDTO();

            Load(dto, activity);

            dto.Analyst   = activity.Analyst;

            return dto;
        }

        private static ModelAccessDTO Create(ModelAccess activity)
        {
            var dto = new ModelAccessDTO();

            Load(dto, activity);

            dto.Analyst   = activity.Analyst;

            return dto;
        }

        private static void Load(ActivityDTO dto, Activity activity)
        {
            dto.DateStart  = activity.DateStart;
            dto.ActivityID = activity.ActivityID;
            dto.BrokerId   = activity.BrokerId;
            dto.BrokerName = activity.BrokerName;
            dto.Comments   = activity.Comments;
            dto.DateEnd    = activity.DateEnd;
            dto.InsertedAt = activity.InsertedAt;
            dto.PushOrPull = activity.PushOrPull;
            dto.Rating     = activity.Rating;
            dto.RefID      = activity.RefID;
            dto.Subject    = activity.Subject;
            dto.Username   = activity.Username;
        }

        public static ActivityDTO Get(Activity activity)
        {
            return _toDTO[activity.GetType()].Invoke(activity);
        }
    }
}