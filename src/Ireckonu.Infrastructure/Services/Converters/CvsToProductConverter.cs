using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using System.Collections.Generic;

namespace Ireckonu.Infrastructure.Services.Converters
{
    public sealed class CvsToProductConverter : IConverter<IFileReader, IAsyncEnumerable<Product>>
    {
        public IAsyncEnumerable<Product> Convert(IFileReader reader)
        {
            // Read first item (HEADER) and skip
            // Read items by items converting to a Product
            throw new System.NotImplementedException();
        }
    }
}
