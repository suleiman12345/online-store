// <copyright file="CartItemDto.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO элемента корзины.
/// Связывает корзину и товар.
/// </summary>
public class CartItemDto
{
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Цена товара.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество товара в корзине.
    /// </summary>
    public int Quantity { get; set; }
}
