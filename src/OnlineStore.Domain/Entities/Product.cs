using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Товар.
/// </summary>
public class Product : BaseEntity
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

    /// <summary>
    /// Навигация к категории.
    /// </summary>
    public Category Category { get; set; } = null!;
}