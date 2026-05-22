using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Shopping cart operations (in-memory, not persisted to database).
/// </summary>
public interface ICartService
{
    /// <summary>Gets all items currently in the cart.</summary>
    Task<IReadOnlyList<CartItemDto>> GetItemsAsync(CancellationToken cancellationToken = default);

    /// <summary>Adds or updates a product quantity in the cart.</summary>
    Task AddItemAsync(
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity,
        CancellationToken cancellationToken = default);

    /// <summary>Updates quantity for an existing cart line.</summary>
    Task UpdateQuantityAsync(Guid productId, int quantity, CancellationToken cancellationToken = default);

    /// <summary>Removes a product from the cart.</summary>
    Task RemoveItemAsync(Guid productId, CancellationToken cancellationToken = default);

    /// <summary>Clears the cart.</summary>
    Task ClearAsync(CancellationToken cancellationToken = default);

    /// <summary>Calculates cart subtotal.</summary>
    Task<decimal> GetTotalAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets total item count in cart.</summary>
    int GetItemCount();
}
