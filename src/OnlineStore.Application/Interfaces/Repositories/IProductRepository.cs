using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с товарами.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Получает список товаров по идентификатору категории.
    /// </summary>
    Task<IReadOnlyList<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}