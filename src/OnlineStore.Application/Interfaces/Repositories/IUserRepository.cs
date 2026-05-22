using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Repository contract for <see cref="User"/> entities.
/// </summary>
public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>Finds a user by email address.</summary>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>Gets a user with profile loaded.</summary>
    Task<User?> GetByIdWithProfileAsync(Guid id, CancellationToken cancellationToken = default);
}
