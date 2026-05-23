using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public interface IProductApiService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync();

    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId);

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(ProductCreateDto dto);
}
