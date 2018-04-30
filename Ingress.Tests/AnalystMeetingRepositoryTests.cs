using System;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Mocks;
using Ingress.Data.Models;
using log4net;
using Ninject;
using NUnit.Framework;

namespace Ingress.Tests
{
    [TestFixture]
    public class AnalystMeetingRepositoryTests
    {
        private StandardKernel _kernel;
        private ILog _log;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target?.Member.DeclaringType?.FullName ?? "unknown"));

            _kernel.Bind<IAnalystMeetingRepository>().To<MockAnalystMeetingRepository>();
            
            _log = _kernel.Get<ILog>();
        }

        [Test]
        public async Task CanAdd()
        {
            var repo = _kernel.Get<IAnalystMeetingRepository>();

            var meeting = new AnalystMeeting()
            {
                Analyst      = "Someone From Somewhere",
                InsertedAt   = DateTime.Now,
                DateStart    = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
                DateEnd      = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0),
                Subject      = "Test meeting",
                BrokerId     = 12,
                BrokerName   = "Morgan stanley",
                CalID        = "testetst",
                Categories   = "Smoe",
                Comments     = "Comments",
                ConvoID      = "testetst",
                GlobalID     = null,
                IsConference = false,
                Organiser    = "Dominic Shaw",
                PushOrPull   = "Push",
                Rating       = 3,
                TimeTaken    = new TimeSpan(0, 0, 30, 0).ToString(),
                Username     = "Dominic Shaw"
            };

            repo.Create(meeting);

            await repo.SaveChanges();

            _log.Info("Created activity: " + meeting.ActivityID);

            Assert.AreNotEqual(meeting.ActivityID, 0);

            repo.Delete(meeting);

            await repo.SaveChanges();
        }

        [Test]
        public async Task CanLoad()
        {
            var repo = _kernel.Get<IAnalystMeetingRepository>();
            var meetings = await repo.GetAll();
            Assert.IsNotEmpty(meetings);
        }
    }
}
