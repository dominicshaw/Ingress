using DevExpress.Mvvm;
using Ingress.WPF.Layouts;
using Ingress.WPF.ViewModels;
using Ingress.WPF.ViewModels.MessengerCommands;

namespace Ingress.WPF.Views
{
    public partial class AllActivitiesGrid
    {
        public AllActivitiesGrid()
        {
            InitializeComponent();
            Messenger.Default.Register<SaveLayoutCommand>(this, SaveLayouts);
        }

        private void SaveLayouts(SaveLayoutCommand cmd)
        {
            if (cmd.ViewModel.GetType() != typeof(ActivitiesViewModel))
                return;

            ControlLayoutManager.SaveControlLayout(allActivitiesGrid.Name, allActivitiesGrid);
        }
    }
}
