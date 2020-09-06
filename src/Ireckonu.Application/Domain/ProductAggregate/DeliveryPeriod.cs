namespace Ireckonu.Application.Domain.ProductAggregate
{
    public sealed class DeliveryPeriod
    {
        public DeliveryPeriod(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; private set; }

        public int Max { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is DeliveryPeriod period &&
                   Min == period.Min &&
                   Max == period.Max;
        }

        public override int GetHashCode()
        {
            int hashCode = 1537547080;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            return hashCode;
        }
    }
}
