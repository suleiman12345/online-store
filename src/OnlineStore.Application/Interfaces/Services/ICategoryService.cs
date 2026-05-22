using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Application service for category operations.
/// </summary>
public interface ICategoryService
{
    /// <summary>Gets all categories.</summary>
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets a category by identifier.</summary>
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Creates a new category.</summary>
    Task<CategoryDto> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing category.</summary>
    Task<CategoryDto?> UpdateAsync(Guid id, CategoryUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Deletes a category.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
