using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        string _statusText = "Ready";

        [ObservableProperty]
        object _activeSidebar; // ActiveSidebar = new ExplorerVM();

        public ObservableCollection<DocumentViewModelBase> Documents { get; } = new ObservableCollection<DocumentViewModelBase>();
        [ObservableProperty]
        DocumentViewModelBase? _activeDocument;

        /// <summary>
        /// IoC constructor for MainWindowViewModel, allowing dependency injection of the logger
        /// </summary>
        /// <param name="logger"></param>
        public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
        {
            _logger = logger;
            _logger.LogInformation("MainWindowViewModel constructor.");
            ActiveSidebar = new object(); // Placeholder for actual sidebar ViewModel
            // Add a sample document to demonstrate the document management system
            var doc = new TextDocumentViewModel() { Title = "Hello.txt", Content = "This is a text document." };
            Documents.Add(doc);
            ActiveDocument = doc;
        }

        [RelayCommand]
        void CloseDocument(DocumentViewModelBase doc)
        {
            if (Documents.Contains(doc))
            {
                Documents.Remove(doc);
                if (ActiveDocument == doc)
                {
                    ActiveDocument = Documents.Count > 0 ? Documents[0] : null;
                }
            }
        }

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