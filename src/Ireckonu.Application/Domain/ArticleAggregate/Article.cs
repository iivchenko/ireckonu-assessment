using Ireckonu.Application.Domain.Common;
using System;

namespace Ireckonu.Application.Domain.ArticleAggregate
{
    public sealed class Article : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }
    }
}
