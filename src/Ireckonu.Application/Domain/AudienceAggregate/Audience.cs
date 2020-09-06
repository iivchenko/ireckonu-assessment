using Ireckonu.Application.Domain.Common;
using System;

namespace Ireckonu.Application.Domain.AudienceAggregate
{
    public sealed class Audience : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
