using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// EF Core реализация репозитория корзины.
/// </summary>
/// <remarks>
/// Инициализирует репозиторий корзины.
/// </remarks>
/// <param name="context">Контекст базы данных.</param>
public class CartRepository(AppDbContext context) : GenericRepository<Cart>(context), ICartRepository
{
    /// <summary>
    /// Получает корзину вместе с позициями и товарами.
    /// </summary>
    /// <param name="id">Идентификатор корзины.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Корзина с элементами или null.</returns>
    public async Task<Cart?> GetCartWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .AsSplitQuery()
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default)
    {
        await _context.CartItems.AddAsync(item, cancellationToken);
    }
}