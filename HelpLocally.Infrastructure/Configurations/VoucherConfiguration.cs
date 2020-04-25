using HelpLocally.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpLocally.Infrastructure.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");

            builder.Property(x => x.OriginalAmount).IsRequired();
            builder.Property(x => x.ExpirationDate).IsRequired();

            builder.HasOne(x => x.Owner);
            builder.HasOne(x => x.Company);

            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.OriginalAmount);
            builder.HasIndex(x => x.CurrentAmount);
            builder.HasIndex(x => x.OwnerId);
        }
    }
}