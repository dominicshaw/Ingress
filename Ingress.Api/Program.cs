using System;
using System.ServiceProcess;
using log4net;
using Microsoft.Owin.Hosting;

namespace Ingress.Api
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        private const int _port = 8199;

        static void Main(string[] args)
        {
            var baseAddress = $"http://*:{_port}/";

            if (args.Length == 1 && args[0] == "/service")
            {
                ServiceBase.Run(new Service(baseAddress));
            }
            else
            {
                _log.Info("Starting Ingress Web API Serivce (console mode)...");

                using (WebApp.Start<Startup>($"http://localhost:{_port}/"))
                {
                    Console.WriteLine("Press any key to stop...");
                    Console.ReadLine();
                }
            }
        }
    }
}
