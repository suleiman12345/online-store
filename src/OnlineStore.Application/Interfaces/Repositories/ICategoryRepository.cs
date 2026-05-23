// <copyright file="ICategoryRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для работы с категориями товаров.
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
