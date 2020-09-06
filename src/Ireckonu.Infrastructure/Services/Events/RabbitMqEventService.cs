using EasyNetQ;
using Ireckonu.Application.Services.Events;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Services.Events
{
    public sealed class RabbitMqEventService : IEventService
    {
        private readonly IBus _bus;

        public RabbitMqEventService(IBus bus)
        {
            _bus = bus;
        }

        public void Dispose()
        {
            _bus.Dispose();
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            await _bus.PublishAsync(@event);
        }
    }
}
