using System;
using System.ServiceProcess;
using log4net;
using Microsoft.Owin.Hosting;

namespace Ingress.Api
{
    internal class Service : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Service));

        private readonly string _baseAddress;
        private IDisposable _svc;

        public Service(string baseAddress)
        {
            _baseAddress = baseAddress;
            ServiceName = "Ingress Web API";
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("Starting Ingress Web API Query Service (window service mode)...");
            _svc = WebApp.Start<Startup>(_baseAddress);
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _log.Info("Stopping Ingress Web API Query Service");
            _svc?.Dispose();
            base.OnStop();
        }
    }
}