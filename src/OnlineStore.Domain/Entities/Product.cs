using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Store product.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Unit price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Available stock quantity.
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Foreign key to category.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Product category.
    /// </summary>
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Tag associations (N:N via <see cref="ProductTag"/>).
    /// </summary>
    public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    /// <summary>
    /// Order line items referencing this product.
    /// </summary>
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
