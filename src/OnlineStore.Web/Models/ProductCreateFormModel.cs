namespace OnlineStore.Web.Models;

/// <summary>
/// Form model for creating a product.
/// </summary>
public class ProductCreateFormModel
{
    /// <summary>
    /// Product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Product price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Selected category id.
    /// </summary>
    public Guid CategoryId { get; set; }
}
