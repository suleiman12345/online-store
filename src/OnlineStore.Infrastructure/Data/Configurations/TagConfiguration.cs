using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="Tag"/>.
/// </summary>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(EntityConstraints.TagNameMaxLength);

        builder.HasIndex(t => t.Name)
            .IsUnique();
    }
}
