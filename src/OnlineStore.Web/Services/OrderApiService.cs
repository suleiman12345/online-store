// <copyright file="OrderApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using System.Net;
using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

public class OrderApiService(HttpClient http) : IOrderApiService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<OrderDto>>("api/orders")
               ?? [];
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<Guid> CreateFromCartAsync(Guid cartId)
    {
        var response = await http.PostAsync($"api/orders/from-cart/{cartId}", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Guid>();
    }
}
