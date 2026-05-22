namespace OnlineStore.Domain.Entities;

/// <summary>
/// Join entity for many-to-many Product ↔ Tag relationship.
/// </summary>
public class ProductTag
{
    /// <summary>
    /// Foreign key to product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Product reference.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Foreign key to tag.
    /// </summary>
    public Guid TagId { get; set; }

    /// <summary>
    /// Tag reference.
    /// </summary>
    public Tag Tag { get; set; } = null!;
}
