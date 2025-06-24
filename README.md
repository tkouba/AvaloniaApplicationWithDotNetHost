# How to implement Using Dependency Injection, Configuration and Hosted Services in Avalonia

This guide will show you step by step how to use Dependency Injection (DI) with Avalonia UI and the MVVM pattern.

Based on tutorial [How To Implement Dependency Injection in Avalonia](https://docs.avaloniaui.net/docs/guides/implementation-guides/how-to-implement-dependency-injection).


## Step 0: Context and Initial Code

Let's assume you have a basic Avalonia application set up. 
If not, you can create one using the Avalonia templates. 
MainWindowViewModel is the ViewModel for the MainWindow. 
A simple implementation would look like this:
```csharp
public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    IBrush _alertColor = Brushes.LightGray;

    [ObservableProperty]
    string _greeting = "Welcome to Avalonia!";

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
    {
        logger.LogInformation("MainWindowViewModel initialized.");
    }
}
```

Change the `MainWindow.axaml.cs` to use the `MainWindowViewModel`:
```csharp
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
```

Create requested services, for example, a simple background service `RandomMonitorService`:
```csharp
public class RandomMonitorService : BackgroundService
{
    private readonly ViewModels.MainWindowViewModel _mvm;
    private readonly Random _rand = new();

    public RandomMonitorService(ViewModels.MainWindowViewModel mvm)
    {
        // Inject the MainWindowViewModel to update the UI
        // Better solution would be to use an event, a message bus 
        // or the WeakReferenceMessenger from CommunityToolkit.Mvvm
        _mvm = mvm;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Dispatcher dispatcher = Dispatcher.UIThread;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);

            var value = _rand.NextDouble();
            if (value > 0.8)
            {
                dispatcher.Invoke(() =>
                    _mvm.AlertColor = Brushes.OrangeRed
                );
            }
            else
            {
                dispatcher.Invoke(() =>
                    _mvm.AlertColor = Brushes.LightGray
                );
            }
        }
    }
}
```

## Step 1: Add Required NuGet Packages
Ensure you have the following NuGet packages installed in your Avalonia project:
```bash
dotnet add package Microsoft.Extensions.Hosting
```
This package provides the hosting and dependency injection capabilities. 
It's dependent on `Microsoft.Extensions.DependencyInjection`, 
`Microsoft.Extensions.Configuration` and `Microsoft.Extensions.Logging`.

## Step 2: Modify App.axaml.cs

Open `App.axaml.cs` and modify the `OnFrameworkInitializationCompleted` method to set up the dependency injection container and register services:
```csharp
IHost? _host;

public override async void OnFrameworkInitializationCompleted()
{
    // Line below is needed to remove Avalonia data validation.
    // Without this line you will get duplicate validations from both Avalonia and CT
    BindingPlugins.DataValidators.RemoveAt(0);

    var builder = Host.CreateApplicationBuilder();
    builder.Services
        .AddSingleton<ViewModels.MainWindowViewModel>()
        .AddSingleton<Views.MainWindow>()
        .AddHostedService<Services.RandomMonitorService>();

    _host = builder.Build();

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        // Only needed if you have background services or need to initialize something asynchronously.
        desktop.ShutdownRequested += DesktopOnShutdownRequested; // <= This will handle the shutdown request and stop the host gracefully.
        await _host.StartAsync(); // <= This will start the host and all services registered in it.

        desktop.MainWindow = _host.Services.GetRequiredService<Views.MainWindow>();
    }

    base.OnFrameworkInitializationCompleted();
}

private async void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
{
    if (_host != null)
        await _host.StopAsync();
}

```

## Remarks

For dialogs and other UI services, you can register them similarly in the DI container.
```csharp
bilder.Services
    .AddTransient<ExportDialog>()
    .AddTransient<Func<ExportDialog>>(sp => sp.GetRequiredService<ExportDialog>);
```

This allows you to inject the dialog into your ViewModels or other services as needed.


```csharp
public SomeView(Func<ExportDialog> exportDialog)
{
    _exportDialog = exportDialog;
}

private async Task ShowExportDialogAsync()
{
    var dialog = _exportDialog();
    if (await dialog.ShowDialog<bool>(this))
    {
        // Handle the result
    }
}
```