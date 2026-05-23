namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO заказа.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания заказа.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Общая стоимость заказа.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Список товаров в заказе.
/// Реализует связь N:N через OrderItem.
/// </summary>
    public List<OrderItemDto> Items { get; set; } = [];
}