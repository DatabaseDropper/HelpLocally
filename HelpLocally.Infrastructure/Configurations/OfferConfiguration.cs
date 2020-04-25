using HelpLocally.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpLocally.Infrastructure.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offerts");

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.TypeId).IsRequired();

            builder.HasOne(x => x.Type).WithMany();

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Price);
            builder.HasIndex(x => x.TypeId);
        }
    }
}