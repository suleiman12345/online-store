namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Stores the current customer context for the UI session.
/// </summary>
public interface IUserSessionService
{
    /// <summary>Gets or sets the active customer identifier.</summary>
    Guid? CurrentUserId { get; set; }
}
