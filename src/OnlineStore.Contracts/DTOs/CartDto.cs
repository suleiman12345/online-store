namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO корзины.
/// </summary>
public class CartDto
{
    /// <summary>
    /// Уникальный идентификатор корзины.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Список товаров в корзине.
/// Реализует связь N:N через CartItem.
/// </summary>
    public List<CartItemDto> Items { get; set; } = [];
}