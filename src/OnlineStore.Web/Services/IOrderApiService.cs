using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public interface IOrderApiService
{
    Task<IReadOnlyList<OrderDto>> GetAllAsync();

    Task<OrderDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateFromCartAsync(Guid cartId);
}
