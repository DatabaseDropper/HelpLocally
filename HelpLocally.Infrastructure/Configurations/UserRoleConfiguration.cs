using HelpLocally.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpLocally.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(bc => new { bc.UserId, bc.RoleId });

            builder
                .HasOne(bc => bc.Role)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.RoleId);

            builder
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}