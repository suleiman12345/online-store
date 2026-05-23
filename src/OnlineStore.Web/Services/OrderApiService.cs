using System.Net;
using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public class OrderApiService(HttpClient http) : IOrderApiService
{
    public async Task<IReadOnlyList<OrderDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<OrderDto>>("api/orders")
               ?? [];
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var response = await http.GetAsync($"api/orders/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }

    public async Task<Guid> CreateFromCartAsync(Guid cartId)
    {
        var response = await http.PostAsync($"api/orders/from-cart/{cartId}", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>();
    }
}
