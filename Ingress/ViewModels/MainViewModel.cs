using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Ingress.Data.Interfaces;
using log4net;

namespace Ingress.ViewModels
{
    public class MainViewModel
    {
        private readonly ILog _log;
        private readonly IAnalystMeetingRepository _analystMeetingRepository;

        public ObservableCollection<AnalystMeetingViewModel> AnalystMeetings { get; } = new ObservableCollection<AnalystMeetingViewModel>();

        public MainViewModel(ILog log, IAnalystMeetingRepository analystMeetingRepository)
        {
            _log = log;
            _analystMeetingRepository = analystMeetingRepository;
        }

        public async Task Start()
        {
            foreach (var m in await _analystMeetingRepository.FindSkipped())
                AnalystMeetings.Add(new AnalystMeetingViewModel(m));
        }
    }
}
