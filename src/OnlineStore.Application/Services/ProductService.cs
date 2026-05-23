// <copyright file="ProductService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Services;

using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Entities;

/// <summary>
/// Сервис работы с товарами.
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        this._productRepository = productRepository;
        this._categoryRepository = categoryRepository;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await this._productRepository.GetAllAsync(cancellationToken);
        var categories = await this._categoryRepository.GetAllAsync(cancellationToken);
        var categoryNames = categories.ToDictionary(x => x.Id, x => x.Name);

        return products.Select(x => MapToDto(x, categoryNames.GetValueOrDefault(x.CategoryId))).ToList();
    }

    /// <inheritdoc/>
    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await this._productRepository.GetByIdAsync(id, cancellationToken);

        if (product == null)
            return null;

        var category = await this._categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken);

        return MapToDto(product, category?.Name);
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(ProductCreateDto dto, CancellationToken cancellationToken = default)
    {
        var category = await this._categoryRepository.GetByIdAsync(dto.CategoryId, cancellationToken);

        if (category == null)
            throw new KeyNotFoundException("Category not found");

        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await this._productRepository.AddAsync(entity, cancellationToken);
        await this._productRepository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Guid id, ProductDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await this._productRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException("Product not found");

        entity.Name = dto.Name;
        entity.Price = dto.Price;
        entity.CategoryId = dto.CategoryId;

        this._productRepository.Update(entity);
        await this._productRepository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await this._productRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return;

        this._productRepository.Remove(entity);
        await this._productRepository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var products = await this._productRepository.GetByCategoryIdAsync(categoryId, cancellationToken);

        return products.Select(x => MapToDto(x, x.Category?.Name)).ToList();
    }

    private static ProductDto MapToDto(Product product, string? categoryName)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = categoryName ?? string.Empty,
        };
    }
}
