using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Infrastructure.Persistence.Configurations;

public class AdministratorConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.Property(t => t.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(t => t.LastName).HasMaxLength(50).IsRequired();
        builder.Property(t => t.PhoneNumber).HasMaxLength(50).IsRequired();

        builder.Property(t => t.ApplicationUserId).IsRequired();

        builder.HasOne<Identity.ApplicationUserIdentity>()
                .WithOne()
                .HasForeignKey<Administrator>(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
