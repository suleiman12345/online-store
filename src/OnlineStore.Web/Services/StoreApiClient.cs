using System.Net;
using System.Net.Http.Json;
using OnlineStore.Application.DTOs;

namespace OnlineStore.Web.Services;

/// <summary>
/// Typed <see cref="HttpClient"/> wrapper for store API endpoints.
/// </summary>
public class StoreApiClient : IStoreApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of <see cref="StoreApiClient"/>.
    /// </summary>
    public StoreApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products", cancellationToken);
        return products ?? [];
    }

    /// <inheritdoc />
    public async Task<ProductDto?> GetProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"api/products/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories", cancellationToken);
        return categories ?? [];
    }

    /// <inheritdoc />
    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/categories", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CategoryDto>(cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> UpdateCategoryAsync(
        Guid id,
        CategoryUpdateDto dto,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CategoryDto>(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"api/categories/{id}", cancellationToken);
        return response.StatusCode != HttpStatusCode.NotFound;
    }

    /// <inheritdoc />
    public async Task<Guid> UpsertUserAsync(CheckoutDto checkout, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/checkout", checkout, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<UserIdResponse>(cancellationToken);
        return result?.Id ?? throw new InvalidOperationException("User id was not returned by the API.");
    }

    /// <inheritdoc />
    public async Task<OrderDto> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", dto, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException(string.IsNullOrWhiteSpace(error) ? "Order creation failed." : error);
        }

        return (await response.Content.ReadFromJsonAsync<OrderDto>(cancellationToken))!;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderDto>> GetOrdersByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var orders = await _httpClient.GetFromJsonAsync<List<OrderDto>>(
            $"api/orders/user/{userId}",
            cancellationToken);

        return orders ?? [];
    }

    private sealed class UserIdResponse
    {
        public Guid Id { get; set; }
    }
}
