using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Domain.ProductAggregate
{
    public sealed class EfProductRepository : IRepository<Product, Guid>
    {
        public Task CreateAsync(Product aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
