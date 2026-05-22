using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Single line item within an order.
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Foreign key to order.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Parent order.
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Foreign key to product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Ordered product.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Quantity ordered.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Price at the time of order.
    /// </summary>
    public decimal Price { get; set; }
}
