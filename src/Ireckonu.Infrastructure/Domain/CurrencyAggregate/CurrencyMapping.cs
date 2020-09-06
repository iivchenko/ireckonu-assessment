using Ireckonu.Application.Domain.CurrencyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ireckonu.Infrastructure.Domain.CurrencyAggregate
{
    public sealed class CurrencyMapping : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
              .ToTable("currencies");

            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Name)
                .HasMaxLength(5);
        }
    }
}
