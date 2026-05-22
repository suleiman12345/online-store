using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Repository contract for <see cref="Product"/> entities.
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>Gets a product with its category loaded.</summary>
    Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Gets all products with categories loaded.</summary>
    Task<IReadOnlyList<Product>> GetAllWithCategoryAsync(CancellationToken cancellationToken = default);
}
