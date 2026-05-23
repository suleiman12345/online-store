// <copyright file="IOrderRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для работы с заказами.
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Получает заказ с позициями и товарами.
    /// </summary>
    Task<Order?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все заказы с позициями и товарами.
    /// </summary>
    Task<IReadOnlyList<Order>> GetAllWithItemsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заказы за указанный период времени.
    /// </summary>
    Task<IReadOnlyList<Order>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
}
