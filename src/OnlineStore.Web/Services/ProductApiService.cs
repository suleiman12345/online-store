using System.Net;
using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public class ProductApiService(HttpClient http) : IProductApiService
{
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<ProductDto>>("api/products")
               ?? [];
    }

    public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId)
    {
        return await http.GetFromJsonAsync<List<ProductDto>>($"api/products?categoryId={categoryId}")
               ?? [];
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var response = await http.GetAsync($"api/products/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }
}
