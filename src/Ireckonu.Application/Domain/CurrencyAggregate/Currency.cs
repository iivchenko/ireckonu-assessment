using Ireckonu.Application.Domain.Common;
using System;

namespace Ireckonu.Application.Domain.CurrencyAggregate
{
    public sealed class Currency : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
