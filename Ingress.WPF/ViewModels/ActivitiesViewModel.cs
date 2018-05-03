using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using Ingress.Data.DataSources;
using Ingress.Data.Interfaces;
using Ingress.WPF.Factories;
using Ingress.WPF.Layouts;
using Ingress.WPF.ViewModels.Data;
using Ingress.WPF.ViewModels.MessengerCommands;

namespace Ingress.WPF.ViewModels
{
    public class ActivitiesViewModel : SelectableViewModel
    {
        private readonly IActivityRepository _repo;
        private readonly ILoadActivityFactory _newActivityFactory;
        private readonly List<Broker> _brokers;

        public ICommand NavigateCommand => new DelegateCommand<ActivityViewModel>(a => Messenger.Default.Send(new NavigationCommand(Where.Activity, a)));

        public ObservableCollection<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();

        public ICommand RestoreLayoutsCommands => new DelegateCommand<GridControl>(grid => ControlLayoutManager.RestoreControlLayout(grid.Name, grid));

        public ActivitiesViewModel(IActivityRepository repo, ILoadActivityFactory newActivityFactory, List<Broker> brokers)
        {
            _repo = repo;
            _newActivityFactory = newActivityFactory;
            _brokers = brokers;
        }

        public async Task Start()
        {
            Activities.Clear();
            foreach (var activity in await _repo.GetAll())
            {
                var avm = _newActivityFactory.Load(activity);

                if (avm.BrokerId.HasValue)
                    avm.Broker = _brokers.Find(b => b.ID == avm.BrokerId.Value);

                Activities.Add(avm);
            }
        }
    }
}