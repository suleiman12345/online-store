// <copyright file="ProductDto.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO товара.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Уникальный идентификатор товара.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Цена товара.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Колличество товара.
    /// </summary>
    public decimal StockQuantity { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Название категории товара.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;
}
