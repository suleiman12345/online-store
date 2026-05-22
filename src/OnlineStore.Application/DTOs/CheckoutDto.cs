namespace OnlineStore.Application.DTOs;

/// <summary>
/// Checkout form data for placing an order.
/// </summary>
public class CheckoutDto
{
    /// <summary>Customer email.</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Customer full name.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Contact phone.</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>Delivery address.</summary>
    public string DeliveryAddress { get; set; } = string.Empty;
}
