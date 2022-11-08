using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Infrastructure.Persistence.Configurations;

public class DonorConfiguration : IEntityTypeConfiguration<Donor>
{
    public void Configure(EntityTypeBuilder<Donor> builder)
    {
        builder.Property(t => t.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(t => t.LastName).HasMaxLength(50).IsRequired();
        builder.Property(t => t.Sex).HasMaxLength(50).IsRequired();
        builder.Property(t => t.JMBG).HasMaxLength(50).IsRequired();
        builder.Property(t => t.State).HasMaxLength(50).IsRequired();
        builder.Property(t => t.HomeAddress).HasMaxLength(50).IsRequired();
        builder.Property(t => t.City).HasMaxLength(50).IsRequired();
        builder.Property(t => t.PhoneNumber).HasMaxLength(50).IsRequired();
        builder.Property(t => t.Occupation).HasMaxLength(50).IsRequired();
        builder.Property(t => t.OccupationInfo).HasMaxLength(50).IsRequired();

        builder.Property(t => t.ApplicationUserId).IsRequired();

        builder.HasOne<Identity.ApplicationUser>()
            .WithOne()
            .HasForeignKey<Donor>(d => d.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
