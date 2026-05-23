using OnlineStore.Contracts.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

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
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
    }

    public async Task<Guid> CreateFromCartAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
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

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<IReadOnlyList<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetAllWithItemsAsync(cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetWithItemsAsync(id, cancellationToken);
        return order is null ? null : MapToDto(order);
    }

    public async Task<IReadOnlyList<OrderDto>> GetByDateRangeAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetByDateRangeAsync(from, to, cancellationToken);
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
