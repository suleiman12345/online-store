using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public class CartApiService(HttpClient http) : ICartApiService
{
    public async Task<CartDto?> GetCartAsync(Guid cartId)
    {
        var response = await http.GetAsync($"api/cart/{cartId}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CartDto>();
    }

    public async Task<CartDto> AddItemAsync(Guid cartId, Guid productId, int quantity = 1)
    {
        var response = await http.PostAsync(
            $"api/cart/{cartId}/items?productId={productId}&quantity={quantity}",
            null);

        response.EnsureSuccessStatusCode();
        return await RefreshCartAsync(cartId);
    }

    public async Task<CartDto> RemoveItemAsync(Guid cartId, Guid productId)
    {
        var response = await http.DeleteAsync($"api/cart/{cartId}/items/{productId}");
        response.EnsureSuccessStatusCode();
        return await RefreshCartAsync(cartId);
    }

    public async Task<CartDto> ClearAsync(Guid cartId)
    {
        var response = await http.DeleteAsync($"api/cart/{cartId}/clear");
        response.EnsureSuccessStatusCode();
        return await RefreshCartAsync(cartId);
    }

    private async Task<CartDto> RefreshCartAsync(Guid cartId)
    {
        return await GetCartAsync(cartId)
               ?? throw new InvalidOperationException("Cart not found after mutation.");
    }
}
