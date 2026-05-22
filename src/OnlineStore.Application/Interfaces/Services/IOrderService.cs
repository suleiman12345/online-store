using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Order management application service.
/// </summary>
public interface IOrderService
{
    /// <summary>Creates an order from explicit line items.</summary>
    Task<OrderDto> CreateAsync(OrderCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Gets order history for a customer.</summary>
    Task<IReadOnlyList<OrderDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>Gets a single order by identifier.</summary>
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Calculates total amount for line items.</summary>
    decimal CalculateTotal(IEnumerable<(decimal unitPrice, int quantity)> lines);
}
