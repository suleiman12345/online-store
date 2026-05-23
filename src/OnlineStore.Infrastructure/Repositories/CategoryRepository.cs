// <copyright file="CategoryRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Infrastructure.Repositories;

using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

/// <summary>
/// EF Core реализация репозитория категорий.
/// </summary>
/// <remarks>
/// Инициализирует репозиторий категорий.
/// </remarks>
/// <param name="context">Контекст базы данных.</param>
public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    /// <inheritdoc/>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
