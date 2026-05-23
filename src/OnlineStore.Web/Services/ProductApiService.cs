// <copyright file="ProductApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using System.Net;
using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

public class ProductApiService(HttpClient http) : IProductApiService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<ProductDto>>("api/products")
               ?? [];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId)
    {
        return await http.GetFromJsonAsync<List<ProductDto>>($"api/products?categoryId={categoryId}")
               ?? [];
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(ProductCreateDto dto)
    {
        var response = await http.PostAsJsonAsync("api/products", dto);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                string.IsNullOrWhiteSpace(body)
                    ? $"Create product failed ({(int)response.StatusCode})."
                    : body);
        }

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        if (id == Guid.Empty)
        {
            throw new InvalidOperationException("API returned empty product id.");
        }

        return id;
    }
}
