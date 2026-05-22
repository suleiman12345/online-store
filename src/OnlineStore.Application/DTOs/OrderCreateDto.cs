namespace OnlineStore.Application.DTOs;

/// <summary>
/// Data required to create a new order.
/// </summary>
public class OrderCreateDto
{
    /// <summary>Customer identifier.</summary>
    public Guid UserId { get; set; }

    /// <summary>Order line items.</summary>
    public IReadOnlyList<OrderLineDto> Items { get; set; } = Array.Empty<OrderLineDto>();
}
