using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.Application.Services;

/// <summary>
/// Scoped storage for the active customer in the UI session.
/// </summary>
public class UserSessionService : IUserSessionService
{
    /// <inheritdoc />
    public Guid? CurrentUserId { get; set; }
}
