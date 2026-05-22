using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Customer order.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Order creation timestamp (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Total order amount (sum of line items).
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Foreign key to user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Customer who placed the order.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Line items in the order.
    /// </summary>
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
