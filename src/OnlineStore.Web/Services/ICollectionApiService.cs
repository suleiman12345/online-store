using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public interface ICollectionApiService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
}
