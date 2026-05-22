using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Category application service implementation.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Initializes a new instance of <see cref="CategoryService"/>.
    /// </summary>
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        return categories.Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        return category is null ? null : MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken = default)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> UpdateAsync(
        Guid id,
        CategoryUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null)
        {
            return null;
        }

        category.Name = dto.Name;
        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(category);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdWithProductsAsync(id, cancellationToken);
        if (category is null)
        {
            return false;
        }

        if (category.Products.Count > 0)
        {
            throw new InvalidOperationException("Cannot delete a category that has products.");
        }

        _categoryRepository.Remove(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static CategoryDto MapToDto(Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name
        };
}
