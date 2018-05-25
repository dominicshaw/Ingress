using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using Ingress.Data.DataSources;
using Ingress.Data.Models;
using Ingress.Data.Repositories;
using Ingress.WPF.Factories;
using Ingress.WPF.Layouts;
using Ingress.WPF.ViewModels.Data;
using Ingress.WPF.ViewModels.MessengerCommands;

namespace Ingress.WPF.ViewModels
{
    public class ActivitiesViewModel : SelectableViewModel
    {
        private readonly ILoadActivityFactory _newActivityFactory;
        private readonly List<Broker> _brokers;

        public ICommand NavigateCommand => new DelegateCommand<ActivityViewModel>(a => Messenger.Default.Send(new NavigationCommand(Where.Activity, a)));
        public ICommand RestoreLayoutsCommands => new DelegateCommand<GridControl>(RestoreLayouts);

        public ObservableCollection<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();

        public ActivitiesViewModel(ILoadActivityFactory newActivityFactory, List<Broker> brokers)
        {
            _newActivityFactory = newActivityFactory;
            _brokers = brokers;
        }

        public async Task Start()
        {
            using (var repo = new ActivityRepository(new IngressContext()))
            {
                Activities.Clear();
                foreach (var activity in await repo.GetAll())
                {
                    var avm = _newActivityFactory.Load(activity);

                    if (avm.BrokerId.HasValue)
                        avm.Broker = _brokers.Find(b => b.ID == avm.BrokerId.Value);

                    Activities.Add(avm);
                }
            }
        }

        private static void RestoreLayouts(GridControl grid)
        {
            ControlLayoutManager.RestoreControlLayout(grid.Name, grid);

            if (string.IsNullOrWhiteSpace(grid.FilterString))
                grid.SetCurrentValue(DataControlBase.FilterStringProperty, "[Username] In ('" + UserPrincipal.Current.DisplayName + "')");
        }
    }
}