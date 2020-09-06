using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ireckonu.Application.Domain.Common
{
    public interface IRepository<TAggregate, TId> where TAggregate
       : IAggregateRoot<TId>
    {
        Task<IEnumerable<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate);

        Task CreateAsync(TAggregate aggregate);

        Task UpdateAsync(TAggregate aggregate);
    }
}
