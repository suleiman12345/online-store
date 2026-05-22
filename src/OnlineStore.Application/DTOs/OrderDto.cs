namespace OnlineStore.Application.DTOs;

/// <summary>
/// Order data transfer object.
/// </summary>
public class OrderDto
{
    /// <summary>Order identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Customer identifier.</summary>
    public Guid UserId { get; set; }

    /// <summary>Order creation time (UTC).</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>Total order amount.</summary>
    public decimal TotalAmount { get; set; }

    /// <summary>Line items in the order.</summary>
    public IReadOnlyList<OrderItemDto> Items { get; set; } = Array.Empty<OrderItemDto>();
}
