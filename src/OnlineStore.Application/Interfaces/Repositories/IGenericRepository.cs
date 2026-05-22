using System.Linq.Expressions;
using OnlineStore.Domain.Common;

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Generic repository contract for CRUD operations.
/// </summary>
/// <typeparam name="T">Entity type derived from <see cref="BaseEntity"/>.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>Gets all entities.</summary>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets an entity by identifier.</summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Finds entities matching a predicate.</summary>
    Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>Adds a new entity.</summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing entity.</summary>
    void Update(T entity);

    /// <summary>Removes an entity.</summary>
    void Remove(T entity);

    /// <summary>Persists pending changes.</summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
