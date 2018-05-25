using System;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Reflection;
using System.Windows;
using DevExpress.Mvvm;
using Ingress.WPF.Layouts;
using Ingress.WPF.ViewModels;
using Ingress.WPF.ViewModels.MessengerCommands;
using log4net;

namespace Ingress.WPF
{
    public partial class MainWindow
    {
        private readonly ILog _log;
        private readonly MainViewModel _model;

        public MainWindow(ILog log, MainViewModel model, WindowLayoutManager windowLayoutManager)
        {
            InitializeComponent();

            Title = string.Format("Ingress v{0} ({1})",
                ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : Assembly.GetExecutingAssembly().GetName().Version + "d",
                ConfigurationManager.ConnectionStrings["IngressDb"].ConnectionString.Contains("LIVESERVER") ? "LIVE" : "TEST");

            log.Info(Title);

            _log = log;
            _model = model;

            DataContext = _model;

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

            windowLayoutManager.ApplyJot(this);

            Messenger.Default.Register<NavigationCommand>(this, CheckShown);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _model.Start();
                Hide();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _model.SelectedView = new ErrorMessageViewModel(ex);
            }
        }

        private void CheckShown(NavigationCommand cmd)
        {
            if (!IsVisible) Show();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (_model.SelectedView != null)
                Messenger.Default.Send(new SaveLayoutCommand(_model.SelectedView));

            if (!ControlLayoutManager.Close())
                _log.Error("Failed to close all layout writers.");

            e.Cancel = true;
            Hide();
        }
    }
}
