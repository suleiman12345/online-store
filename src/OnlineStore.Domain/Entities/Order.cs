using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Заказ.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Дата создания заказа.
/// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Позиции заказа.
/// </summary>
    public ICollection<OrderItem> Items { get; set; } = [];
}