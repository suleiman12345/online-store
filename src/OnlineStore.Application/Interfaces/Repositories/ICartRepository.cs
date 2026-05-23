using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для работы с корзинами пользователей.
/// </summary>
public interface ICartRepository : IRepository<Cart>
{
    /// <summary>
    /// Получает корзину вместе с товарами.
    /// </summary>
    Task<Cart?> GetCartWithItemsAsync(Guid cartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет позицию в корзину.
    /// </summary>
    Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default);
}