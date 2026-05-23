using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Сервис работы с товарами.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Получает все товары.
    /// </summary>
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает товар по Id.
    /// </summary>
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает товары по категории.
    /// </summary>
    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт товар.
    /// </summary>
    Task<Guid> CreateAsync(ProductDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет товар.
    /// </summary>
    Task UpdateAsync(Guid id, ProductDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет товар.
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}