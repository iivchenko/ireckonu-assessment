using System;

namespace Ireckonu.Application.Domain.ProductAggregate
{
    public sealed class Price
    {
        public Price(Guid currencyId, decimal value)
        {
            CurrencyId = currencyId;
            Value = value;
        }

        public Guid CurrencyId { get; private set; }

        public decimal Value { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Price price &&
                   CurrencyId == price.CurrencyId &&
                   Value == price.Value;
        }

        public override int GetHashCode()
        {
            int hashCode = -771160272;
            hashCode = hashCode * -1521134295 + CurrencyId.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
