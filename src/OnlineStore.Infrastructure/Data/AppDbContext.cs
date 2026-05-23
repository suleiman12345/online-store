// <copyright file="AppDbContext.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

/// <summary>
/// Entity Framework database context for the online store.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AppDbContext"/>.
/// </remarks>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Product categories.</summary>
    public DbSet<Category> Categories => this.Set<Category>();

    /// <summary>Cart of products.</summary>
    public DbSet<Cart> Carts => this.Set<Cart>();

    /// <summary>Items of cart.</summary>
    public DbSet<CartItem> CartItems => this.Set<CartItem>();


    /// <summary>Store products.</summary>
    public DbSet<Product> Products => this.Set<Product>();

    /// <summary>Customer orders.</summary>
    public DbSet<Order> Orders => this.Set<Order>();

    /// <summary>Order line items.</summary>
    public DbSet<OrderItem> OrderItems => this.Set<OrderItem>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
