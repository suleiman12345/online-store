// <copyright file="CartsController.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;

/// <summary>
/// Контроллер корзины.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        this._cartService = cartService;
    }

    /// <summary>
    /// Создать корзину.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CancellationToken cancellationToken)
    {
        var cartId = await this._cartService.CreateAsync(cancellationToken);
        return this.CreatedAtAction(nameof(this.Get), new { cartId }, cartId);
    }

    /// <summary>
    /// Получить корзину.
    /// </summary>
    [HttpGet("{cartId:guid}")]
    public async Task<ActionResult<CartDto>> Get(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        var cart = await this._cartService.GetAsync(cartId, cancellationToken);

        if (cart is null)
            return this.NotFound();

        return this.Ok(cart);
    }

    /// <summary>
    /// Добавить товар в корзину.
    /// </summary>
    [HttpPost("{cartId:guid}/items")]
    public async Task<IActionResult> AddItem(
        Guid cartId,
        [FromQuery] Guid productId,
        [FromQuery] int quantity,
        CancellationToken cancellationToken)
    {
        await this._cartService.AddItemAsync(cartId, productId, quantity, cancellationToken);
        return this.NoContent();
    }

    /// <summary>
    /// Удалить товар из корзины.
    /// </summary>
    [HttpDelete("{cartId:guid}/items/{productId:guid}")]
    public async Task<IActionResult> RemoveItem(
        Guid cartId,
        Guid productId,
        CancellationToken cancellationToken)
    {
        await this._cartService.RemoveItemAsync(cartId, productId, cancellationToken);
        return this.NoContent();
    }

    /// <summary>
    /// Очистить корзину.
    /// </summary>
    [HttpDelete("{cartId:guid}/clear")]
    public async Task<IActionResult> Clear(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        await this._cartService.ClearAsync(cartId, cancellationToken);
        return this.NoContent();
    }
}
