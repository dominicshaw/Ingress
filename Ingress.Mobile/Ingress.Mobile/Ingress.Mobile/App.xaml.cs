using System;
using System.Collections.Generic;
using Ingress.Mobile.Helpers;
using Ingress.Mobile.Services;
using Ingress.Mobile.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Ingress.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenter.Start("ios=fe9dd6e5-6bb8-4949-a504-d6fe223c419d;",
                typeof(Analytics), typeof(Crashes));

            DependencyService.Register<IApi, HttpApi>();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            if (await Crashes.HasCrashedInLastSessionAsync())
            {
                var crashReport = await Crashes.GetLastSessionCrashReportAsync();
                Reporter.ReportException(crashReport.Exception ?? new Exception("Crash from last run; no exception info."), new Dictionary<string, string> { { "Source", "AppCrash" } });
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
