using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// EF Core repository for <see cref="Order"/> entities.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    /// <summary>
    /// Initializes a new instance of <see cref="OrderRepository"/>.
    /// </summary>
    public OrderRepository(AppDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Order?> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Order>> GetByUserIdWithItemsAsync(Guid userId, CancellationToken cancellationToken = default) =>
        await DbSet
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
}
