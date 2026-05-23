using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с категориями товаров.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}