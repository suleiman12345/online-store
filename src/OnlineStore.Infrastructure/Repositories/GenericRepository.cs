using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Common;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// Generic EF Core repository implementation.
/// </summary>
/// <typeparam name="T">Entity type derived from <see cref="BaseEntity"/>.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    /// <summary>Database context.</summary>
    protected readonly AppDbContext Context;

    /// <summary>Entity DbSet.</summary>
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// Initializes a new instance of <see cref="GenericRepository{T}"/>.
    /// </summary>
    public GenericRepository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public virtual void Update(T entity) => DbSet.Update(entity);

    /// <inheritdoc />
    public virtual void Remove(T entity) => DbSet.Remove(entity);

    /// <inheritdoc />
    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Context.SaveChangesAsync(cancellationToken);
}
