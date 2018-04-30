using System.ComponentModel;
using Ingress.WPF.ViewModels;

namespace Ingress.WPF
{
    public partial class MainWindow
    {
        private readonly MainViewModel _model;

        public MainWindow(MainViewModel model)
        {
            InitializeComponent();

            _model = model;
            DataContext = _model;

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _model.Start();
        }
    }
}
