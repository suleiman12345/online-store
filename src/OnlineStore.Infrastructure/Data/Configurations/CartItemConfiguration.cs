using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core конфигурация элемента корзины.
/// </summary>
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
{
    builder.ToTable("CartItems");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Quantity)
        .IsRequired();

    builder.Property(x => x.UnitPrice)
        .HasPrecision(18, 2);

    builder.HasOne(x => x.Cart)
        .WithMany(x => x.Items)
        .HasForeignKey(x => x.CartId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(x => x.Product)
        .WithMany()
        .HasForeignKey(x => x.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(x => new { x.CartId, x.ProductId })
        .IsUnique();
}
}