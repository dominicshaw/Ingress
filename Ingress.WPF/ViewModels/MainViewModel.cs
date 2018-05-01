using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ingress.Data.DataSources;
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
        private readonly IDataSourcesRepository _dataSourcesRepository;
        private readonly INewActivityFactory _newActivityFactory;

        private object _selectedView;
        private List<string> _analysts;
        private List<Broker> _brokers;
        private bool _working;

        public List<string> Analysts
        {
            get => _analysts;
            set
            {
                if (Equals(value, _analysts)) return;
                _analysts = value;
                OnPropertyChanged();
            }
        }

        public List<Broker> Brokers
        {
            get => _brokers;
            set
            {
                if (Equals(value, _brokers)) return;
                _brokers = value;
                OnPropertyChanged();
            }
        }

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

        public bool Working
        {
            get => _working;
            set
            {
                if (value == _working) return;
                _working = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ILog log, IActivityRepository activityRepository, IDataSourcesRepository dataSourcesRepository, INewActivityFactory newActivityFactory)
        {
            _log = log;

            _activityRepository = activityRepository;
            _dataSourcesRepository = dataSourcesRepository;
            _newActivityFactory = newActivityFactory;

            _newActivityFactory.NewActivity += NewActivity;
        }

        private void NewActivity(ActivityViewModel activity)
        {
            SelectedView = activity;
        }

        public async Task Start()
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                Working = true;
                var vm = new ActivitiesViewModel(_activityRepository);
                SelectedView = vm;

                await vm.Start();

                Analysts = await _dataSourcesRepository.GetAnalysts();
                Brokers = await _dataSourcesRepository.GetBrokers();
            }
            finally
            {
                Working = false;
                sw.Stop();

                _log.Info($"MainViewModel loaded in {sw.ElapsedMilliseconds}ms.");
            }
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
