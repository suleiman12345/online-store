// <copyright file="CollectionsController.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;

/// <summary>
/// Контроллер коллекций (категорий товаров).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CollectionsController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }

    /// <summary>
    /// Получить все коллекции (категории).
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await this._categoryService.GetAllAsync(cancellationToken);
        return this.Ok(result);
    }

    /// <summary>
    /// Получить коллекцию по id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await this._categoryService.GetByIdAsync(id, cancellationToken);

        if (result is null)
            return this.NotFound();

        return this.Ok(result);
    }

    /// <summary>
    /// Создать коллекцию.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CategoryDto dto,
        CancellationToken cancellationToken)
    {
        var id = await this._categoryService.CreateAsync(dto, cancellationToken);

        return this.CreatedAtAction(
            nameof(this.GetById),
            new { id },
            id);
    }

    /// <summary>
    /// Обновить коллекцию.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] CategoryDto dto,
        CancellationToken cancellationToken)
    {
        await this._categoryService.UpdateAsync(id, dto, cancellationToken);
        return this.NoContent();
    }

    /// <summary>
    /// Удалить коллекцию.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await this._categoryService.DeleteAsync(id, cancellationToken);
        return this.NoContent();
    }
}
