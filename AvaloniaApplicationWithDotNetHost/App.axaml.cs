using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AvaloniaApplicationWithDotNetHost
{
    public partial class App : Application
    {
        IHost? _host;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

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
    }
}