using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.Api.Controllers;

/// <summary>
/// REST API for customer user operations used during checkout.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of <see cref="UsersController"/>.
    /// </summary>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Finds or creates a user from checkout form data.
    /// </summary>
    [HttpPost("checkout")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] CheckoutDto dto, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetOrCreateForCheckoutAsync(dto, cancellationToken);
        return Ok(new { id = userId });
    }
}
