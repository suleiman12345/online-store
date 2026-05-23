// <copyright file="OrdersController.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;

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
        this._orderService = orderService;
    }

    /// <summary>
    /// Получить все заказы.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await this._orderService.GetAllAsync(cancellationToken);
        return this.Ok(result);
    }

    /// <summary>
    /// Получить заказ по id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await this._orderService.GetByIdAsync(id, cancellationToken);

        if (result is null)
            return this.NotFound();

        return this.Ok(result);
    }

    /// <summary>
    /// Создать заказ из корзины.
    /// </summary>
    [HttpPost("from-cart/{cartId:guid}")]
    public async Task<ActionResult<Guid>> CreateFromCart(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        var orderId = await this._orderService.CreateFromCartAsync(cartId, cancellationToken);

        return this.CreatedAtAction(
            nameof(this.GetById),
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
        var result = await this._orderService.GetByDateRangeAsync(from, to, cancellationToken);
        return this.Ok(result);
    }
}
