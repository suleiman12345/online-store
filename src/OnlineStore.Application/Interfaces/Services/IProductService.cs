using OnlineStore.Application.DTOs;

namespace OnlineStore.Application.Interfaces.Services;

/// <summary>
/// Application service for product operations.
/// </summary>
public interface IProductService
{
    /// <summary>Gets all products.</summary>
    Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets a product by identifier.</summary>
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Creates a new product.</summary>
    Task<ProductDto> CreateAsync(ProductCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing product.</summary>
    Task<ProductDto?> UpdateAsync(Guid id, ProductUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Deletes a product.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
