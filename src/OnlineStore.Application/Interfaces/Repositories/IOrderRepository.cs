using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Repository contract for <see cref="Order"/> entities.
/// </summary>
public interface IOrderRepository : IGenericRepository<Order>
{
    /// <summary>Gets an order with line items and products loaded.</summary>
    Task<Order?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Gets all orders for a user with line items loaded.</summary>
    Task<IReadOnlyList<Order>> GetByUserIdWithItemsAsync(Guid userId, CancellationToken cancellationToken = default);
}
