using HelpLocally.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpLocally.Infrastructure.Configurations
{
    public class OfferTypeConfiguration : IEntityTypeConfiguration<OfferType>
    {
        public void Configure(EntityTypeBuilder<OfferType> builder)
        {
            builder.ToTable("OfferTypes");

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Id);
        }
    }
}