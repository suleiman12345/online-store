namespace OnlineStore.Web.Services;

/// <summary>
/// Stores only the cart identifier on the client; cart data comes from the API.
/// </summary>
public interface ICartIdService
{
    Task<Guid> GetCartIdAsync();

    Task EnsureInitializedAsync();
}
