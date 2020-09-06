using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Domain.ArticleAggregate
{
    public sealed class EfArticleRepository : IRepository<Article, Guid>
    {
        private readonly IreckonuContext _context;

        public EfArticleRepository(IreckonuContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> FindAsync(Expression<Func<Article, bool>> predicate)
        {
            return await _context.Articles.Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
