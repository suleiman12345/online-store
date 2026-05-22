using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Order application service implementation.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="OrderService"/>.
    /// </summary>
    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    /// <inheritdoc />
    public async Task<OrderDto> CreateAsync(OrderCreateDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.Items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain at least one item.");
        }

        var orderId = Guid.NewGuid();
        var orderItems = new List<OrderItem>();
        var lineTotals = new List<(decimal unitPrice, int quantity)>();

        foreach (var line in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(line.ProductId, cancellationToken)
                ?? throw new InvalidOperationException($"Product '{line.ProductId}' was not found.");

            if (line.Quantity <= 0)
            {
                throw new InvalidOperationException("Quantity must be positive.");
            }

            if (product.StockQuantity < line.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'.");
            }

            product.StockQuantity -= line.Quantity;
            _productRepository.Update(product);

            orderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = product.Id,
                Quantity = line.Quantity,
                Price = product.Price
            });

            lineTotals.Add((product.Price, line.Quantity));
        }

        var order = new Order
        {
            Id = orderId,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = CalculateTotal(lineTotals),
            OrderItems = orderItems
        };

        await _orderRepository.AddAsync(order, cancellationToken);
        await _orderRepository.SaveChangesAsync(cancellationToken);

        var created = await _orderRepository.GetByIdWithItemsAsync(orderId, cancellationToken);
        return MapToDto(created!);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetByUserIdWithItemsAsync(userId, cancellationToken);
        return orders.Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id, cancellationToken);
        return order is null ? null : MapToDto(order);
    }

    /// <inheritdoc />
    public decimal CalculateTotal(IEnumerable<(decimal unitPrice, int quantity)> lines) =>
        lines.Sum(l => l.unitPrice * l.quantity);

    private static OrderDto MapToDto(Order order) =>
        new()
        {
            Id = order.Id,
            UserId = order.UserId,
            CreatedAt = order.CreatedAt,
            TotalAmount = order.TotalAmount,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? string.Empty,
                Quantity = oi.Quantity,
                UnitPrice = oi.Price
            }).ToList()
        };
}
