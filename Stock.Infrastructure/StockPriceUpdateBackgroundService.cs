using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Infrastructure
{
    public class StockPriceUpdateBackgroundService: BackgroundService
    {
        private readonly ILogger<StockPriceUpdateBackgroundService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        public StockPriceUpdateBackgroundService(ILogger<StockPriceUpdateBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            await DoWork(cancellation);
        }
        private async Task DoWork(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    var scopedProccesingService = scope.ServiceProvider.GetRequiredService<IPriceUpdateProcessingService>();
                    await scopedProccesingService.DoWork(cancellation);
                }
                await Task.Delay(30000, cancellation);
            }
        }

    }
}
