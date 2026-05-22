using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// EF Core repository for <see cref="Category"/> entities.
/// </summary>
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    /// <summary>
    /// Initializes a new instance of <see cref="CategoryRepository"/>.
    /// </summary>
    public CategoryRepository(AppDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Category?> GetByIdWithProductsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
}
