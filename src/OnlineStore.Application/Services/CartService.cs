using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.Application.Services;

/// <summary>
/// In-memory shopping cart backed by <see cref="Dictionary{Guid, CartItemDto}"/>.
/// </summary>
public class CartService : ICartService
{
    private readonly Dictionary<Guid, CartItemDto> _items = new();

    /// <inheritdoc />
    public Task<IReadOnlyList<CartItemDto>> GetItemsAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<CartItemDto>>(_items.Values.OrderBy(i => i.ProductName).ToList());

    /// <inheritdoc />
    public Task AddItemAsync(
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        if (_items.TryGetValue(productId, out var existing))
        {
            existing.Quantity += quantity;
            return Task.CompletedTask;
        }

        _items[productId] = new CartItemDto
        {
            ProductId = productId,
            ProductName = productName,
            UnitPrice = unitPrice,
            Quantity = quantity
        };

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateQuantityAsync(Guid productId, int quantity, CancellationToken cancellationToken = default)
    {
        if (!_items.ContainsKey(productId))
        {
            throw new InvalidOperationException($"Product '{productId}' is not in the cart.");
        }

        if (quantity <= 0)
        {
            _items.Remove(productId);
        }
        else
        {
            _items[productId].Quantity = quantity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RemoveItemAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        _items.Remove(productId);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        _items.Clear();
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<decimal> GetTotalAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(_items.Values.Sum(i => i.LineTotal));

    /// <inheritdoc />
    public int GetItemCount() => _items.Values.Sum(i => i.Quantity);
}
