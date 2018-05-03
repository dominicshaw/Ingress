using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using Ingress.Data.DataSources;
using Ingress.Data.Interfaces;
using Ingress.WPF.Factories;
using Ingress.WPF.ViewModels.Data;
using Ingress.WPF.ViewModels.MessengerCommands;
using JetBrains.Annotations;
using log4net;

namespace Ingress.WPF.ViewModels
{
    [UsedImplicitly]
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ILog _log;

        private readonly IActivityRepository _activityRepository;
        private readonly IDataSourcesRepository _dataSourcesRepository;
        private readonly ILoadActivityFactory _newActivityFactory;

        private SelectableViewModel _selectedView;
        private List<string> _analysts;
        private List<Broker> _brokers;
        private bool _working;
        private string _flashMessage;

        public ICommand CancelCommand => new AsyncCommand<ActivityViewModel>(Cancel, _ => SelectedView is ActivityViewModel);
        public ICommand SaveCommand => new AsyncCommand(Save, () => SelectedView is ActivityViewModel a && a.IsValid);

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

        public SelectableViewModel SelectedView
        {
            get => _selectedView;
            set
            {
                if (Equals(value, _selectedView)) return;
                _selectedView = value;
                OnPropertyChanged(nameof(CancelCommand));
                OnPropertyChanged(nameof(SaveCommand));
                OnPropertyChanged();
            }
        }

        public string FlashMessage
        {
            get => _flashMessage;
            set
            {
                if (value == _flashMessage) return;
                _flashMessage = value;
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

        public ICommand LayoutsCommands => new DelegateCommand<SelectableViewModel>(LayoutChange);

        private static void LayoutChange(SelectableViewModel from)
        {
            if (from != null) Messenger.Default.Send(new SaveLayoutCommand(from));
        }

        public MainViewModel(ILog log, IActivityRepository activityRepository, IDataSourcesRepository dataSourcesRepository, ILoadActivityFactory newActivityFactory)
        {
            _log = log;

            _activityRepository = activityRepository;
            _dataSourcesRepository = dataSourcesRepository;
            _newActivityFactory = newActivityFactory;

            Messenger.Default.Register<NavigationCommand>(this, async cmd => await Navigate(cmd));
        }

        public async Task Start()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            try
            {
                Working = true;

                if (Analysts == null || Brokers == null)
                {
                    Analysts = await _dataSourcesRepository.GetAnalysts();
                    Brokers = await _dataSourcesRepository.GetBrokers();
                }
                
                var vm = new ActivitiesViewModel(_activityRepository, _newActivityFactory, Brokers);
                SelectedView = vm;
                
                await Task.Delay(1000); // TODO - the date control is fucking up right here; when the view is changed, it sets the values to mindate, which actually changes the VM too

                await vm.Start();
            }
            finally
            {
                Working = false;
                sw.Stop();

                _log.Info($"MainViewModel loaded in {sw.ElapsedMilliseconds}ms.");
            }
        }

        private async Task Navigate(NavigationCommand cmd)
        {
            try
            {
                switch (cmd.GoWhere)
                {
                    case Where.List:
                        await Start();
                        break;
                    case Where.Activity:
                        SelectedView = cmd.Activity;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                SelectedView = new ErrorMessageViewModel(ex);
            }
        }
        
        private async Task Cancel(ActivityViewModel activity)
        {
            try
            {
                if (activity.ActivityID != 0)
                    _activityRepository.CancelChanges(activity.GetModel());

                await Start();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                SelectedView = new ErrorMessageViewModel(ex);
            }
        }

        private async Task Save()
        {
            try
            { 
                var name = string.Empty;

                if (SelectedView is ActivityViewModel activity)
                {
                    var model = activity.GetModel();

                    if (model.ActivityID == 0)
                        _activityRepository.Create(model);
                    else
                        _activityRepository.Update(model);

                    await _activityRepository.SaveChanges();

                    name = activity.Subject;
                }

                await Start();

                FlashMessage = $"Successfully saved interaction \'{name}\'.";
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                SelectedView = new ErrorMessageViewModel(ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}