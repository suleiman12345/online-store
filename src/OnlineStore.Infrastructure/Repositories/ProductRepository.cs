using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// EF Core repository for <see cref="Product"/> entities.
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    /// <summary>
    /// Initializes a new instance of <see cref="ProductRepository"/>.
    /// </summary>
    public ProductRepository(AppDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet
            .Include(p => p.Category)
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Product>> GetAllWithCategoryAsync(CancellationToken cancellationToken = default) =>
        await DbSet
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
}
