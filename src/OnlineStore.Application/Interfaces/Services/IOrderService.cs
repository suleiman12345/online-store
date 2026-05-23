using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Сервис работы с заказами.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Получает все заказы.
    /// </summary>
    Task<IReadOnlyList<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заказ по Id.
    /// </summary>
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт заказ из корзины.
    /// </summary>
    Task<Guid> CreateFromCartAsync(Guid cartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заказы по диапазону дат.
    /// </summary>
    Task<IReadOnlyList<OrderDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
}