namespace OnlineStore.Application.DTOs;

/// <summary>
/// Single line in an order creation request.
/// </summary>
public class OrderLineDto
{
    /// <summary>Product identifier.</summary>
    public Guid ProductId { get; set; }

    /// <summary>Quantity to order.</summary>
    public int Quantity { get; set; }
}
