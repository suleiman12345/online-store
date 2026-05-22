namespace OnlineStore.Application.DTOs;

/// <summary>
/// Payload for creating a product.
/// </summary>
public class ProductCreateDto
{
    /// <summary>Product name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Product description.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Unit price.</summary>
    public decimal Price { get; set; }

    /// <summary>Available stock quantity.</summary>
    public int StockQuantity { get; set; }

    /// <summary>Category identifier.</summary>
    public Guid CategoryId { get; set; }
}
