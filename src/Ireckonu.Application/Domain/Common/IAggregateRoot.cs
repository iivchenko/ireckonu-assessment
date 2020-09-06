namespace Ireckonu.Application.Domain.Common
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }
    }
}
