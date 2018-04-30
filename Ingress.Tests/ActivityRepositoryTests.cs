using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.Data.Mocks;
using Ninject;
using NUnit.Framework;

namespace Ingress.Tests
{
    [TestFixture]
    public class ActivityRepositoryTests
    {
        private StandardKernel _kernel;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IActivityRepository>().To<MockActivityRepository>();
        }

        [Test]
        public async Task CanLoad()
        {
            var repo = _kernel.Get<IActivityRepository>();
            var meetings = await repo.GetAll();
            Assert.IsNotEmpty(meetings);
        }
    }
}