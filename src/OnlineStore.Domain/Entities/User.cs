using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;

/// <summary>
/// Store customer.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Extended profile (1:1).
    /// </summary>
    public UserProfile? Profile { get; set; }

    /// <summary>
    /// Orders placed by the user.
    /// </summary>
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
