// <copyright file="CartItem.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Domain.Entities;

using OnlineStore.Domain.Common;

/// <summary>
/// Элемент корзины.
/// Связь Cart ↔ Product (N:N).
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Идентификатор корзины.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Корзина.
    /// </summary>
    public Cart Cart { get; set; } = null!;

    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Товар.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Количество.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена на момент добавления (snapshot).
    /// </summary>
    public decimal UnitPrice { get; set; }
}
