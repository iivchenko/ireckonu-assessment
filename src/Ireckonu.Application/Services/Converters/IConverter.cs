namespace Ireckonu.Application.Services.Converters
{
    public interface IConverter<TSource, TTarget>
    {
        TTarget Convert(TSource source);
    }
}
