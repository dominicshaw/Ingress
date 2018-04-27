using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows;
using Ingress.Data.Interfaces;
using Ingress.Data.Repositories;
using log4net;
using Ninject;

namespace Ingress
{
    public partial class App
    {
        private readonly StandardKernel _kernel = new StandardKernel();

        protected override void OnStartup(StartupEventArgs e)
        {
            InitialiseLogs();

            var log = LogManager.GetLogger(GetType());

            Current.DispatcherUnhandledException +=
                (s, ex) => log.Fatal("Dispatcher Unhandled Exception: {0}", ex.Exception);
            AppDomain.CurrentDomain.UnhandledException +=
                (s, ex) => log.Fatal(ex.ExceptionObject);

            base.OnStartup(e);

            _kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target?.Member.DeclaringType?.FullName));
            _kernel.Bind<IAnalystMeetingRepository>().To<AnalystMeetingRepository>();
            _kernel.Bind<ICompanyMeetingRepository>().To<CompanyMeetingRepository>();

            Start();
        }

        private void Start()
        {
            MainWindow = _kernel.Get<MainWindow>();

            MainWindow.Show();
            MainWindow.Focus();
        }

        private static void InitialiseLogs()
        {
            GlobalContext.Properties["username"] = Environment.UserName;
            GlobalContext.Properties["version"] = ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version + "d";

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
