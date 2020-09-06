using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ;
using Ireckonu.Application.Commands.ProcessFile;
using Ireckonu.Application.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ireckonu.FileProcessingHost.Services
{
    public sealed class RabbitMqBackgroundService : IHostedService
    {
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;

        private IBus _bus;

        public RabbitMqBackgroundService(
            IConfiguration configuration, 
            IServiceProvider services)
        {
            _configuration = configuration;
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _bus = RabbitHutch.CreateBus(_configuration.GetConnectionString("RabbitMq"));

            _bus.SubscribeAsync<TemporaryFileUploadedEvent>("Ireckonu.FileProcessor", HandleTemporaryFileUploadedEvent);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _bus.Dispose();

            return Task.CompletedTask;
        }

        private async Task HandleTemporaryFileUploadedEvent(TemporaryFileUploadedEvent @event)
        {
            using (var scope = _services.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var command = mapper.Map<ProcessFileCommand>(@event);

                await mediator.Send(command);
            }
        }
    }
}
