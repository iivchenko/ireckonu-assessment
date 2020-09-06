using Ireckonu.Application.Domain.AudienceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ireckonu.Infrastructure.Domain.AudienceAggregate
{
    public sealed class AudienceMapping : IEntityTypeConfiguration<Audience>
    {
        public void Configure(EntityTypeBuilder<Audience> builder)
        {
            builder
              .ToTable("audiences");

            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.Name)
                .HasMaxLength(25);
        }
    }
}
