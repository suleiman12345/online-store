using OnlineStore.Application.DTOs;

namespace OnlineStore.Web.Services;

/// <summary>
/// HTTP client for the Online Store REST API.
/// </summary>
public interface IStoreApiClient
{
    /// <summary>Gets all products.</summary>
    Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets a product by identifier.</summary>
    Task<ProductDto?> GetProductAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Gets all categories.</summary>
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>Creates a category.</summary>
    Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Updates a category.</summary>
    Task<CategoryDto?> UpdateCategoryAsync(Guid id, CategoryUpdateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Deletes a category.</summary>
    Task<bool> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Finds or creates a user from checkout data.</summary>
    Task<Guid> UpsertUserAsync(CheckoutDto checkout, CancellationToken cancellationToken = default);

    /// <summary>Creates an order.</summary>
    Task<OrderDto> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken = default);

    /// <summary>Gets orders for a user.</summary>
    Task<IReadOnlyList<OrderDto>> GetOrdersByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
