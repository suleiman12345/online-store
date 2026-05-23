// <copyright file="Order.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Domain.Entities;

using OnlineStore.Domain.Common;

/// <summary>
/// Заказ.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Дата создания заказа.
/// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Позиции заказа.
/// </summary>
    public ICollection<OrderItem> Items { get; set; } = [];
}
