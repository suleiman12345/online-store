using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Product category.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Products in this category.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
