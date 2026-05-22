namespace OnlineStore.Application.DTOs;

/// <summary>
/// Shopping cart line item.
/// </summary>
public class CartItemDto
{
    /// <summary>Product identifier.</summary>
    public Guid ProductId { get; set; }

    /// <summary>Product display name.</summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>Unit price.</summary>
    public decimal UnitPrice { get; set; }

    /// <summary>Quantity in cart.</summary>
    public int Quantity { get; set; }

    /// <summary>Line subtotal.</summary>
    public decimal LineTotal => UnitPrice * Quantity;
}
