using System.Threading.Tasks;

namespace Ireckonu.Application.Services.Events
{
    public interface IEventService
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}
