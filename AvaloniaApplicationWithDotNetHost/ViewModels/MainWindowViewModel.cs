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

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
        {
            logger.LogInformation("MainWindowViewModel initialized.");
        }
    }
}