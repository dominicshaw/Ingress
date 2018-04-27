using Ingress.Data.Interfaces;
using Ingress.Data.Models;
using Ingress.Data.Repositories;
using NUnit.Framework;

namespace Ingress.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void CanLoadAnything()
        {
            IAnalystMeetingRepository repo = new AnalystMeetingRepository(new IngressContext());
            var data = repo.FindSkipped().GetAwaiter().GetResult();
        }
    }
}
