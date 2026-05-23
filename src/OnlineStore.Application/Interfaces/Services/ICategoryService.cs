using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Сервис работы с категориями.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Получает все категории.
    /// </summary>
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает категорию по Id.
    /// </summary>
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт новую категорию.
    /// </summary>
    Task<Guid> CreateAsync(CategoryDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет категорию.
    /// </summary>
    Task UpdateAsync(Guid id, CategoryDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет категорию.
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}