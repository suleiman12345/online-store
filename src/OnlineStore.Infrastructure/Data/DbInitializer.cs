using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Data;

/// <summary>
/// Optional database seed data.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Seeds default categories when the database is empty.
    /// </summary>
    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Categories.AnyAsync(cancellationToken))
        {
            return;
        }

        context.Categories.AddRange(
            new Category { Id = Guid.NewGuid(), Name = "Electronics" },
            new Category { Id = Guid.NewGuid(), Name = "Clothing" },
            new Category { Id = Guid.NewGuid(), Name = "Books" });

        await context.SaveChangesAsync(cancellationToken);
    }
}
