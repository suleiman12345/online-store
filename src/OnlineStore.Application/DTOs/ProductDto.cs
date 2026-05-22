namespace OnlineStore.Application.DTOs;

/// <summary>
/// Product data transfer object.
/// </summary>
public class ProductDto
{
    /// <summary>Product identifier.</summary>
    public Guid Id { get; set; }

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

    /// <summary>Category name.</summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>Tag names associated with the product.</summary>
    public IReadOnlyList<string> Tags { get; set; } = Array.Empty<string>();
}
