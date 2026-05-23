// <copyright file="ProductRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Data;

/// <summary>
/// EF Core реализация репозитория товаров.
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    /// <summary>
    /// Инициализирует репозиторий товаров.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await this._context.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получает товар вместе с категорией.
    /// </summary>
    /// <param name="id">Идентификатор товара.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Товар с категорией или null.</returns>
    public async Task<Product?> GetWithCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this._context.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
