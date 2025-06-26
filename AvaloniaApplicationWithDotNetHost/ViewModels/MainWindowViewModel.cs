using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace AvaloniaApplicationWithDotNetHost.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        IBrush _alertColor = Brushes.Transparent;

        [ObservableProperty]
        string _greeting = "Welcome to Avalonia!";

        /// <summary>
        /// IoC constructor for MainWindowViewModel, allowing dependency injection of the logger
        /// </summary>
        /// <param name="logger"></param>
        public MainWindowViewModel(ILogger<MainWindowViewModel> logger) : this()
        {
            logger.LogInformation("MainWindowViewModel initialized.");
        }

        /// <summary>
        /// Design-time constructor for MainWindowViewModel, used by the XAML designer
        /// </summary>
        public MainWindowViewModel()
        {
        }
    }
}