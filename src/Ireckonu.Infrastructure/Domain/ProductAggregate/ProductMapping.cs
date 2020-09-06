using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ireckonu.Infrastructure.Domain.ProductAggregate
{
    public sealed class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
              .ToTable("products");

            builder
                .HasKey(o => o.Id);

            builder
              .HasAlternateKey(o => o.Key);

            builder
                .HasOne<Article>()
                .WithMany()
                .HasForeignKey(p => p.ArticleId);

            builder
                .HasOne<Audience>()
                .WithMany()
                .HasForeignKey(p => p.AudienceId);

            builder
              .Property(o => o.Description)
              .HasMaxLength(50);

            builder
              .OwnsOne(o => o.Size);

            builder
               .OwnsOne(o => o.Color);

            builder
               .OwnsOne(o => o.DeliveryPeriod);

            builder
               .OwnsOne(o => o.DiscountPrice);

            builder
              .OwnsOne(o => o.Price);
        }
    }
}
