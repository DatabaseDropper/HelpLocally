using HelpLocally.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpLocally.Infrastructure.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Nip).IsRequired();
            builder.Property(x => x.BankAccountNumber).IsRequired();

            builder.HasIndex(x => x.Name);
        }
    }
}