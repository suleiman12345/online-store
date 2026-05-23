// <copyright file="Cart.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Domain.Entities;

using OnlineStore.Domain.Common;

/// <summary>
/// Корзина (одна на систему).
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Элементы корзины.
    /// </summary>
    public List<CartItem> Items { get; set; } = new();

    /// <summary>
    /// Общая стоимость корзины.
    /// </summary>
    public decimal TotalPrice { get; set; }
}
