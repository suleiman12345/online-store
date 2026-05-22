using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Repository contract for <see cref="Category"/> entities.
/// </summary>
public interface ICategoryRepository : IGenericRepository<Category>
{
    /// <summary>Gets a category with its products loaded.</summary>
    Task<Category?> GetByIdWithProductsAsync(Guid id, CancellationToken cancellationToken = default);
}
