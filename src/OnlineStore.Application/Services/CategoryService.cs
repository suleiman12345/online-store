// <copyright file="CategoryService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Services;

using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Entities;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await this._categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await this._categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(CategoryDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await this._categoryRepository.AddAsync(entity, cancellationToken);
        await this._categoryRepository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Guid id, CategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = await this._categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category == null)
            throw new KeyNotFoundException("Category not found");

        category.Name = dto.Name;

        this._categoryRepository.Update(category);
        await this._categoryRepository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await this._categoryRepository.DeleteAsync(id, cancellationToken);
        await this._categoryRepository.SaveChangesAsync(cancellationToken);
    }
}
