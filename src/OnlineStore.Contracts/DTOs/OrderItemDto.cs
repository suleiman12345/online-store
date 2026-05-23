// <copyright file="OrderItemDto.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Contracts.DTOs;

/// <summary>
/// DTO элемента заказа.
/// Связывает заказ и товар.
/// </summary>
public class OrderItemDto
{   

    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public Guid OrderId { get; set; }
    
    /// <summary>
    /// Идентификатор товара.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Цена товара на момент заказа.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество товара.
    /// </summary>
    public int Quantity { get; set; }
}
