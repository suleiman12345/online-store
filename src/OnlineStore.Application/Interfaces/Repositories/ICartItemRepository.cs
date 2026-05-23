using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для элементов корзины.
/// </summary>
public interface ICartItemRepository : IRepository<CartItem>
{
}