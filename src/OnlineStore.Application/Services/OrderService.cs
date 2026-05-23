// <copyright file="OrderService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Services;

using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Entities;

/// <summary>
/// Сервис работы с заказами.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository)
    {
        this._orderRepository = orderRepository;
        this._cartRepository = cartRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateFromCartAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        if (cart.Items.Count == 0)
            throw new InvalidOperationException("Cart is empty");

        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            CreatedAt = DateTime.UtcNow,
            Items = cart.Items.Select(x => new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.UnitPrice > 0 ? x.UnitPrice : x.Product?.Price ?? 0,
            }).ToList(),
        };

        await this._orderRepository.AddAsync(order, cancellationToken);
        await this._orderRepository.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var orders = await this._orderRepository.GetAllWithItemsAsync(cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await this._orderRepository.GetWithItemsAsync(id, cancellationToken);
        return order is null ? null : MapToDto(order);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetByDateRangeAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        var orders = await this._orderRepository.GetByDateRangeAsync(from, to, cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    private static OrderDto MapToDto(Order order)
    {
        var items = order.Items.Select(x => new OrderItemDto
        {
            OrderId = order.Id,
            ProductId = x.ProductId,
            ProductName = x.Product?.Name ?? string.Empty,
            Price = x.Price,
            Quantity = x.Quantity,
        }).ToList();

        return new OrderDto
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Items = items,
            TotalPrice = items.Sum(x => x.Price * x.Quantity),
        };
    }
}
