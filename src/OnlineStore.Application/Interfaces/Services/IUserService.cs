using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Customer user operations.
/// </summary>
public interface IUserService
{
    /// <summary>Finds or creates a user from checkout data.</summary>
    Task<Guid> GetOrCreateForCheckoutAsync(CheckoutDto checkout, CancellationToken cancellationToken = default);
}
