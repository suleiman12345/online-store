using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="UserProfile"/> (1:1 with <see cref="User"/>).
/// </summary>
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");

        builder.HasKey(p => p.UserId);

        builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(EntityConstraints.PhoneMaxLength);

        builder.Property(p => p.DefaultAddress)
            .IsRequired()
            .HasMaxLength(EntityConstraints.AddressMaxLength);

        builder.HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
