using Avalonia.Controls;

namespace AvaloniaApplicationWithDotNetHost.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModels.MainWindowViewModel viewModel) : this()
        {
            DataContext = viewModel;            
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}