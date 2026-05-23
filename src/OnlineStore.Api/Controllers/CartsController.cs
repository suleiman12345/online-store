using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Api.Controllers;

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
        _cartService = cartService;
    }

    /// <summary>
    /// Получить корзину.
    /// </summary>
    [HttpGet("{cartId:guid}")]
    public async Task<ActionResult<CartDto>> Get(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetAsync(cartId, cancellationToken);

        if (cart is null)
            return NotFound();

        return Ok(cart);
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
        await _cartService.AddItemAsync(cartId, productId, quantity, cancellationToken);
        return NoContent();
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
        await _cartService.RemoveItemAsync(cartId, productId, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Очистить корзину.
    /// </summary>
    [HttpDelete("{cartId:guid}/clear")]
    public async Task<IActionResult> Clear(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        await _cartService.ClearAsync(cartId, cancellationToken);
        return NoContent();
    }
}