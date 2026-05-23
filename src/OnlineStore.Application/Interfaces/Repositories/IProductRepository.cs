// <copyright file="IProductRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для работы с товарами.
/// </summary>
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Получает список товаров по идентификатору категории.
    /// </summary>
    Task<IReadOnlyList<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}
