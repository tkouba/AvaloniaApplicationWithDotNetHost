using Avalonia.Controls;

namespace AvaloniaApplicationWithDotNetHost.Views
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// IoC constructor for MainWindow, allowing dependency injection of the ViewModel
        /// </summary>
        public MainWindow(ViewModels.MainWindowViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        /// <summary>
        /// Parameterless constructor for XAML designer support
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}