using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="User"/>.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(EntityConstraints.EmailMaxLength);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(EntityConstraints.FullNameMaxLength);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId);
    }
}
