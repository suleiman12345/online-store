namespace OnlineStore.Domain.Entities;

/// <summary>
/// Extended profile for a store customer (1:1 with <see cref="User"/>).
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Primary key and foreign key to <see cref="User"/>.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Associated user.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Contact phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Default delivery address.
    /// </summary>
    public string DefaultAddress { get; set; } = string.Empty;
}
