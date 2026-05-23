using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core конфигурация корзины.
/// </summary>
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
{
    builder.ToTable("Carts");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.TotalPrice)
        .HasPrecision(18, 2);

    builder.HasMany(x => x.Items)
        .WithOne(x => x.Cart)
        .HasForeignKey(x => x.CartId)
        .OnDelete(DeleteBehavior.Cascade);
}
}