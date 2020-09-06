namespace Ireckonu.Application.Domain.ProductAggregate
{
    public sealed class Color
    {
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; private set; }

        public byte G { get; private set; }

        public byte B { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Color color &&
                   R == color.R &&
                   G == color.G &&
                   B == color.B;
        }

        public override int GetHashCode()
        {
            int hashCode = -1520100960;
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            return hashCode;
        }
    }
}
