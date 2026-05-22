using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Entity Framework database context for the online store.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of <see cref="AppDbContext"/>.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>Product categories.</summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>Store products.</summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>Store customers.</summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>Customer orders.</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Order line items.</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>Customer profiles.</summary>
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    /// <summary>Product tags.</summary>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <summary>Product-tag associations.</summary>
    public DbSet<ProductTag> ProductTags => Set<ProductTag>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
