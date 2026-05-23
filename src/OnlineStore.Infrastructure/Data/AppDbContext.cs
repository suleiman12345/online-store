using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Entity Framework database context for the online store.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AppDbContext"/>.
/// </remarks>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Product categories.</summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>Cart of products.</summary>
    public DbSet<Cart> Carts => Set<Cart>();

    /// <summary>Items of cart.</summary>
    public DbSet<CartItem> CartItems => Set<CartItem>();


    /// <summary>Store products.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>Customer orders.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Order line items.</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
