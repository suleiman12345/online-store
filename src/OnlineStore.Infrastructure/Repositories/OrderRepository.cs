using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// EF Core реализация репозитория заказов.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    /// <summary>
    /// Инициализирует репозиторий заказов.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsSplitQuery()
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsSplitQuery()
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetByDateRangeAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsSplitQuery()
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .Where(x => x.CreatedAt >= from && x.CreatedAt <= to)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}