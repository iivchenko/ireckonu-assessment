using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.CurrencyAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Domain.CurrencyAggregate
{
    public sealed class EfCurrencyRepository : IRepository<Currency, Guid>
    {
        private readonly IreckonuContext _context;

        public EfCurrencyRepository(IreckonuContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Currency>> FindAsync(Expression<Func<Currency, bool>> predicate)
        {
            return await _context.Currencies.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(Currency currency)
        {
            _context.Entry(currency).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
