using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using Ingress.WPF.ViewModels.Data;

namespace Ingress.WPF.ViewModels
{
    public class ActivitiesViewModel
    {
        private readonly IActivityRepository _repo;

        public ObservableCollection<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();

        public ActivitiesViewModel(IActivityRepository repo)
        {
            _repo = repo;
        }

        public async Task Start()
        {
            foreach (var m in await _repo.GetAll())
                Activities.Add(new ActivityViewModel(_repo, m));
        }
    }
}