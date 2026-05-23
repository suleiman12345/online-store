// <copyright file="CategoryDto.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO категории товаров.
/// Отображает категорию и связанные товары.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Уникальный идентификатор категории.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название категории.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Список товаров категории.
    /// Связь 1:N.
    /// </summary>
    public List<ProductDto> Products { get; set; } = [];
}
