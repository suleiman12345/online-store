using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий для элементов заказа.
/// </summary>
public interface IOrderItemRepository : IRepository<OrderItem>
{
}