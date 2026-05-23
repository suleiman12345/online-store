// <copyright file="OrderItem.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Domain.Entities;

using OnlineStore.Domain.Common;

/// <summary>
/// Элемент заказа.
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Заказ.
    /// </summary>
    public Order Order { get; set; } = null!;

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
    /// Цена на момент покупки.
    /// </summary>
    public decimal Price { get; set; }
}
