using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ireckonu.FileProcessingHost
{
    public sealed class RabbitMqBackgroundService : BackgroundService
    {
        private readonly ILogger<RabbitMqBackgroundService> _logger;

        public RabbitMqBackgroundService(ILogger<RabbitMqBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
