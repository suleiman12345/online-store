using Microsoft.EntityFrameworkCore;
using OnlineStore.Contracts;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Инициализатор базы данных.
/// Применяет миграции и заполняет начальными данными.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Инициализирует базу данных: применяет миграции и seed-данные.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public static async Task InitializeAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync())
        {
            return;
        }

        var categories = new List<Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Electronics" },
            new() { Id = Guid.NewGuid(), Name = "Books" },
            new() { Id = Guid.NewGuid(), Name = "Clothing" },
            new() { Id = Guid.NewGuid(), Name = "Cars" },
        };

        await context.Categories.AddRangeAsync(categories);

        var products = new List<Product>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Laptop",
                Price = 1200,
                CategoryId = categories[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Smartphone",
                Price = 800,
                CategoryId = categories[0].Id,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Book - Clean Code",
                Price = 30,
                CategoryId = categories[1].Id,
            },
        };

        await context.Products.AddRangeAsync(products);

        var cart = new Cart
        {
            Id = StoreDefaults.DefaultCartId,
            Items = [],
        };

        await context.Carts.AddAsync(cart);

        await context.SaveChangesAsync();
    }
}