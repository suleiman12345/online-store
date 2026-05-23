namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO создания товара.
/// </summary>
public class ProductCreateDto
{
    /// <summary>
    /// Название товара.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Цена товара.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }
}
