using Ireckonu.Application.Domain.Common;
using System;

namespace Ireckonu.Application.Domain.ProductAggregate
{
    public sealed class Product : IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Description { get; set; }

        public Guid ArticleId { get; set; }

        public Guid AudienceId { get; set; }

        public Price Price { get; set; }

        public Price DiscountPrice { get; set; }

        public DeliveryPeriod DeliveryPeriod { get; set; }

        public Size Size { get; set; }

        public Color Color { get; set; }
    }
}
