using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

namespace OnlineStore.Web.Services;

public class CollectionApiService(HttpClient http) : ICollectionApiService
{
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<CategoryDto>>("api/collections")
               ?? [];
    }
}
