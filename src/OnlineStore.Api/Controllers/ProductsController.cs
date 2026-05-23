// <copyright file="ProductsController.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;

/// <summary>
/// Контроллер товаров.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        this._productService = productService;
    }

    /// <summary>
    /// Получить все товары.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(
        [FromQuery] Guid? categoryId,
        CancellationToken cancellationToken)
    {
        var result = categoryId.HasValue
            ? await this._productService.GetByCategoryAsync(categoryId.Value, cancellationToken)
            : await this._productService.GetAllAsync(cancellationToken);

        return this.Ok(result);
    }

    /// <summary>
    /// Получить товар по Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await this._productService.GetByIdAsync(id, cancellationToken);

        if (result == null)
            return this.NotFound();

        return this.Ok(result);
    }

    /// <summary>
    /// Создать товар.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(ProductCreateDto dto, CancellationToken cancellationToken)
    {
        var id = await this._productService.CreateAsync(dto, cancellationToken);

        return this.CreatedAtAction(nameof(this.GetById), new { id }, id);
    }

    /// <summary>
    /// Обновить товар.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto dto, CancellationToken cancellationToken)
    {
        await this._productService.UpdateAsync(id, dto, cancellationToken);
        return this.NoContent();
    }

    /// <summary>
    /// Удалить товар.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await this._productService.DeleteAsync(id, cancellationToken);
        return this.NoContent();
    }
}
