using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Корзина (одна на систему).
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Элементы корзины.
    /// </summary>
    public List<CartItem> Items { get; set; } = new();

    /// <summary>
    /// Общая стоимость корзины.
    /// </summary>
    public decimal TotalPrice { get; set; }
}