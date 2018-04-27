using Ingress.ViewModels;

namespace Ingress
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
        }

        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _model.Start();
        }
    }
}
