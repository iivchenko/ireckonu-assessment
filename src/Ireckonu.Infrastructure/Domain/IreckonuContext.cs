using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.CurrencyAggregate;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Infrastructure.Domain.ArticleAggregate;
using Ireckonu.Infrastructure.Domain.AudienceAggregate;
using Ireckonu.Infrastructure.Domain.CurrencyAggregate;
using Ireckonu.Infrastructure.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ireckonu.Infrastructure.Domain
{
    public sealed class IreckonuContext : DbContext
    {
        public IreckonuContext(DbContextOptions<IreckonuContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Audience> Audiences { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMapping());
            builder.ApplyConfiguration(new ArticleMapping());
            builder.ApplyConfiguration(new AudienceMapping());
            builder.ApplyConfiguration(new CurrencyMapping());
        }
    }
}
