using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Api.Controllers;

/// <summary>
/// Health check endpoint for container orchestration.
/// </summary>
[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Returns API health status.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() => Ok(new { status = "healthy" });
}
