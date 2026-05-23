using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Категория товаров.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Название категории.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Товары категории (many-to-many)
    /// </summary>
    public List<Product> Products { get; set; } = [];
}