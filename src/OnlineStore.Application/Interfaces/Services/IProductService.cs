// <copyright file="IProductService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Services;

using OnlineStore.Contracts.DTOs;

/// <summary>
/// Сервис работы с товарами.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Получает все товары.
    /// </summary>
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает товар по Id.
    /// </summary>
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает товары по категории.
    /// </summary>
    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт товар.
    /// </summary>
    Task<Guid> CreateAsync(ProductCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет товар.
    /// </summary>
    Task UpdateAsync(Guid id, ProductDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет товар.
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
