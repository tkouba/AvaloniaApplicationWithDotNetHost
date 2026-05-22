using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplicationWithDotNetHost.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        readonly ILogger<MainWindowViewModel> _logger;
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
            _logger = logger;
            _logger.LogInformation("MainWindowViewModel constructor.");
        }

        /// <summary>
        /// Design-time constructor for MainWindowViewModel, used by the XAML designer
        /// </summary>
        public MainWindowViewModel() { /* NOP */ }

        public override Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("MainWindowviewModel initialized.");
            return base.InitializeAsync(cancellationToken);
        }

        public override Task LoadAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("MainWindowviewModel loaded.");
            return base.LoadAsync(cancellationToken);
        }

        public override Task UnloadAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("MainWindowviewModel unloaded.");
            return base.UnloadAsync(cancellationToken);
        }
    }
}