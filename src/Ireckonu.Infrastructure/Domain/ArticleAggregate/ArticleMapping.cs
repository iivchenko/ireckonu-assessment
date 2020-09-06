using Ireckonu.Application.Domain.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ireckonu.Infrastructure.Domain.ArticleAggregate
{
    public sealed class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
              .ToTable("articles");

            builder
                .HasKey(o => o.Id);

            builder
              .Property(o => o.Code)
              .HasMaxLength(25);

            builder
                .Property(o => o.Name)
                .HasMaxLength(25);
        }
    }
}
