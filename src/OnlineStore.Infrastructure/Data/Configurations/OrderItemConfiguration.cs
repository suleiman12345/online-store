using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data.Configurations;

/// <summary>
/// Fluent API configuration for <see cref="OrderItem"/>.
/// </summary>
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.OrderId)
            .IsRequired();

        builder.Property(oi => oi.ProductId)
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.Price)
            .IsRequired()
            .HasPrecision(EntityConstraints.PricePrecision, EntityConstraints.PriceScale);
    }
}
