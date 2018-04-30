using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.WPF.Factories;
using Ingress.WPF.ViewModels.Data;
using JetBrains.Annotations;
using log4net;

namespace Ingress.WPF.ViewModels
{
    [UsedImplicitly]
    public sealed class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly ILog _log;

        private readonly IActivityRepository _activityRepository;
        private readonly INewActivityFactory _newActivityFactory;

        private object _selectedView;

        public object SelectedView
        {
            get => _selectedView;
            set
            {
                if (Equals(value, _selectedView)) return;
                _selectedView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ILog log, IActivityRepository activityRepository, INewActivityFactory newActivityFactory)
        {
            _log = log;
            _activityRepository = activityRepository;
            _newActivityFactory = newActivityFactory;

            _newActivityFactory.NewActivity += NewActivity;
        }

        private void NewActivity(ActivityViewModel activity)
        {
            SelectedView = activity;
        }

        public async Task Start()
        {
            var vm = new ActivitiesViewModel(_activityRepository);
            SelectedView = vm;
            await vm.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _newActivityFactory.NewActivity -= NewActivity;
        }
    }
}
