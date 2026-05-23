using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public interface ICartApiService
{
    Task<CartDto?> GetCartAsync(Guid cartId);

    Task<CartDto> AddItemAsync(Guid cartId, Guid productId, int quantity = 1);

    Task<CartDto> RemoveItemAsync(Guid cartId, Guid productId);

    Task<CartDto> ClearAsync(Guid cartId);
}
