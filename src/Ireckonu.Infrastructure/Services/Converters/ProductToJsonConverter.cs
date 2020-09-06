using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Ireckonu.Infrastructure.Services.Converters
{
    public sealed class ProductToJsonConverter : IConverter<Product, IAsyncEnumerable<string>>
    {
        public IAsyncEnumerable<string> Convert(Product source)
        {
            return new[] { JsonConvert.SerializeObject(source) }.ToAsyncEnumerable();
        }
    }
}
