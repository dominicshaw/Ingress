using System;
using System.Deployment.Application;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Ingress.Data.Interfaces;
using Ingress.Data.Mocks;
using Ingress.Data.Repositories;
using Ingress.WPF.Factories;
using log4net;
using Microsoft.Win32;
using Ninject;

namespace Ingress.WPF
{
    public partial class App
    {        
        private readonly StandardKernel _kernel = new StandardKernel();
        private TaskbarIcon _notifyIcon;

        private readonly bool _mock = false;

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

            if (_mock)
            {
                _kernel.Bind<IActivityRepository>().To<MockActivityRepository>(); // MockActivityRepository
                _kernel.Bind<IDataSourcesRepository>().To<MockDataSourcesRepository>(); // MockDataSourcesRepository
            }
            else
            {
                _kernel.Bind<IActivityRepository>().To<ActivityRepository>(); // MockActivityRepository
                _kernel.Bind<IDataSourcesRepository>().To<DataSourcesRepository>(); // MockDataSourcesRepository
            }

            _kernel.Bind<ILoadActivityFactory>().To<LoadActivityFactory>();

            Start();

            Task.Run(() => CheckRegistry(log));
        }

        private static void InitialiseLogs()
        {
            GlobalContext.Properties["username"] = Environment.UserName;
            GlobalContext.Properties["version"] = ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version + "d";

            log4net.Config.XmlConfigurator.Configure();
        }

        private void Start()
        {
            MainWindow = _kernel.Get<MainWindow>();
            MainWindow.Show();
        }

        private static void CheckRegistry(ILog log)
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                
                if (key == null)
                {
                    log.Error("Could not find startup reg key for user " + Environment.UserName);
                    return;
                }

                key.SetValue("Ingress", "\\\\domain.com\\Deploy\\Published\\Ingress\\Ingress.application"); // Assembly.GetExecutingAssembly().Location
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
