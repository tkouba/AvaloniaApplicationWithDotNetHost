using Avalonia.Media;
using Avalonia.Threading;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplicationWithDotNetHost.Services
{
    public class RandomMonitorService : BackgroundService
    {
        private readonly ViewModels.MainWindowViewModel _mvm;
        private readonly Random _rand = new();

        public RandomMonitorService(ViewModels.MainWindowViewModel mvm)
        {
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
                        _mvm.AlertColor = Brushes.Transparent
                    );
                }
            }
        }
    }
}
