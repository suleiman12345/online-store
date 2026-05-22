namespace OnlineStore.Application.DTOs;

/// <summary>
/// Order line item data transfer object.
/// </summary>
public class OrderItemDto
{
    /// <summary>Line item identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Product identifier.</summary>
    public Guid ProductId { get; set; }

    /// <summary>Product name at order time.</summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>Quantity ordered.</summary>
    public int Quantity { get; set; }

    /// <summary>Unit price at order time.</summary>
    public decimal UnitPrice { get; set; }

    /// <summary>Line total (quantity × unit price).</summary>
    public decimal LineTotal => Quantity * UnitPrice;
}
