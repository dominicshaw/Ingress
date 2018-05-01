using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Ingress.Data.Interfaces;
using Ingress.Data.Mocks;
using Ingress.Data.Repositories;
using Ingress.WPF.Factories;
using log4net;
using Ninject;

namespace Ingress.WPF
{
    public partial class App
    {        
        private readonly StandardKernel _kernel = new StandardKernel();
        private TaskbarIcon _notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            InitialiseLogs();

            var log = LogManager.GetLogger(GetType());

            Current.DispatcherUnhandledException +=
                (s, ex) => log.Fatal("Dispatcher Unhandled Exception: {0}", ex.Exception);
            AppDomain.CurrentDomain.UnhandledException +=
                (s, ex) => log.Fatal(ex.ExceptionObject);

            base.OnStartup(e);

            _notifyIcon = (TaskbarIcon) FindResource("NotifyIcon");

            _kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target?.Member.DeclaringType?.FullName));

            if (false)
            {
                _kernel.Bind<IActivityRepository>().To<MockActivityRepository>(); // MockActivityRepository
                _kernel.Bind<IDataSourcesRepository>().To<MockDataSourcesRepository>(); // MockDataSourcesRepository
            }
            else
            {
                _kernel.Bind<IActivityRepository>().To<ActivityRepository>(); // MockActivityRepository
                _kernel.Bind<IDataSourcesRepository>().To<DataSourcesRepository>(); // MockDataSourcesRepository
            }

            _kernel.Bind<INewActivityFactory>().To<NewActivityFactory>();

            Start();
        }

        private void Start()
        {
            MainWindow = _kernel.Get<MainWindow>();
            MainWindow.Show();
        }

        private static void InitialiseLogs()
        {
            GlobalContext.Properties["username"] = Environment.UserName;
            GlobalContext.Properties["version"] = ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version + "d";

            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
