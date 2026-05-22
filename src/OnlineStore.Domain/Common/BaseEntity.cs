namespace OnlineStore.Domain.Common;

/// <summary>
/// Base entity with a unique identifier.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; set; }
}
