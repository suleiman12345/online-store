// <copyright file="HealthController.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Get() => this.Ok(new { status = "healthy" });
}
