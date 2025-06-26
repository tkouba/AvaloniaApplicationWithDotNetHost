using Avalonia.Media;
using Avalonia.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplicationWithDotNetHost.Services
{
    /// <summary>
    /// Service that simulates a random monitor for demonstration purposes.
    /// For demonstration purposes uses MainWindowViewModel singleton as dependency.
    /// </summary>
    public class RandomMonitorService : BackgroundService
    {
        private readonly ViewModels.MainWindowViewModel _mvm;
        private readonly ILogger _logger;
        private readonly Random _rand = new();

        /// <summary>
        /// Constructor for RandomMonitorService with IoC dependency injection.
        /// </summary>
        /// <param name="mvm">IoC injected viewmodel which is defined as singleton in IoC</param>
        /// <param name="logger">ILogger from HostApplicationBuilder</param>
        public RandomMonitorService(ViewModels.MainWindowViewModel mvm, ILogger<RandomMonitorService> logger)
        {
            _mvm = mvm;
            _logger = logger;
        }

        /// <summary>
        /// Background service execution method that runs in a loop until cancellation is requested.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RandomMonitorService started at: {time}", DateTimeOffset.Now);
            Dispatcher dispatcher = Dispatcher.UIThread;
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                var value = _rand.NextDouble();
                // Update the ViewModel property directly to change the alert color based on the random value.
                // This is not the best practice, but it works for demonstration purposes.
                // Dispatcher is used to ensure that the UI thread is updated safely.
                // In a real application, you would want to use a more robust messaging system, for example,
                // WeakReferenceMessenger from CommunityToolkit.Mvvm.Messaging to avoid tight coupling between
                // the service and the ViewModel.
                _logger.LogInformation("Random value generated: {value}", value);
                if (value > 0.8)
                {
                    await dispatcher.InvokeAsync(() =>
                        _mvm.AlertColor = Brushes.OrangeRed
                    );
                }
                else
                {
                    await dispatcher.InvokeAsync(() =>
                        _mvm.AlertColor = Brushes.Transparent
                    );
                }
            }
        }
    }
}
