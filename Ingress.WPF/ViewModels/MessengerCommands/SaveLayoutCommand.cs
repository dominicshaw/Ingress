namespace Ingress.WPF.ViewModels.MessengerCommands
{
    public class SaveLayoutCommand
    {
        public SaveLayoutCommand(SelectableViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        
        public SelectableViewModel ViewModel { get; }
    }
}