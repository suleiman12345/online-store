using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Customer user application service.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="UserService"/>.
    /// </summary>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> GetOrCreateForCheckoutAsync(CheckoutDto checkout, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = checkout.Email.Trim().ToLowerInvariant();
        var existing = await _userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existing is not null)
        {
            existing.FullName = checkout.FullName.Trim();
            existing.Profile ??= new UserProfile { UserId = existing.Id };
            existing.Profile.Phone = checkout.Phone.Trim();
            existing.Profile.DefaultAddress = checkout.DeliveryAddress.Trim();

            _userRepository.Update(existing);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return existing.Id;
        }

        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Email = normalizedEmail,
            FullName = checkout.FullName.Trim(),
            Profile = new UserProfile
            {
                UserId = userId,
                Phone = checkout.Phone.Trim(),
                DefaultAddress = checkout.DeliveryAddress.Trim()
            }
        };

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
