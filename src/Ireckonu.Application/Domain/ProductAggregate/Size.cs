namespace Ireckonu.Application.Domain.ProductAggregate
{
    public sealed class Size
    {
        public Size(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Size size &&
                   Value == size.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}
