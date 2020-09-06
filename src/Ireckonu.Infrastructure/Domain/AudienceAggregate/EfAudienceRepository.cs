using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Domain.AudienceAggregate
{
    public sealed class EfAudienceRepository : IRepository<Audience, Guid>
    {
        private readonly IreckonuContext _context;

        public EfAudienceRepository(IreckonuContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Audience audience)
        {
            await _context.Audiences.AddAsync(audience);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Audience>> FindAsync(Expression<Func<Audience, bool>> predicate)
        {
            return await _context.Audiences.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(Audience audience)
        {
            _context.Entry(audience).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
