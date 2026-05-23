// <copyright file="IGenericRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

/// <summary>
/// Базовый контракт репозитория для выполнения CRUD-операций над сущностями.
/// </summary>
/// <typeparam name="T">Тип доменной сущности.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получает сущность по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность или null, если не найдена.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все сущности.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список всех сущностей (read-only).</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет новую сущность в контекст.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    /// <param name="entity">Сущность с обновлёнными данными.</param>
    void Update(T entity);

    /// <summary>
    /// Удаляет сущность из контекста.
    /// </summary>
    /// <param name="entity">Удаляемая сущность.</param>
    void Remove(T entity);

    /// <summary>
    /// Сохраняет изменения в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
