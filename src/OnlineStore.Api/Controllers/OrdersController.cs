using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.Api.Controllers;

/// <summary>
/// Контроллер заказов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Получить все заказы.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _orderService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить заказ по id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _orderService.GetByIdAsync(id, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Создать заказ из корзины.
    /// </summary>
    [HttpPost("from-cart/{cartId:guid}")]
    public async Task<ActionResult<Guid>> CreateFromCart(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        var orderId = await _orderService.CreateFromCartAsync(cartId, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = orderId },
            orderId);
    }

    /// <summary>
    /// Получить заказы за период.
    /// </summary>
    [HttpGet("range")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetByDateRange(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        CancellationToken cancellationToken)
    {
        var result = await _orderService.GetByDateRangeAsync(from, to, cancellationToken);
        return Ok(result);
    }
}