using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="Product"/>.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(EntityConstraints.NameMaxLength);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(EntityConstraints.DescriptionMaxLength);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(EntityConstraints.PricePrecision, EntityConstraints.PriceScale);

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .IsRequired();

        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
