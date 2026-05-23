// <copyright file="Category.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Domain.Entities;

using OnlineStore.Domain.Common;

/// <summary>
/// Категория товаров.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Название категории.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Товары категории (many-to-many)
    /// </summary>
    public List<Product> Products { get; set; } = [];
}
