// <copyright file="CartApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

public class CartApiService(HttpClient http) : ICartApiService
{
    /// <inheritdoc/>
    public async Task<Guid> CreateCartAsync()
    {
        var response = await http.PostAsync("api/cart", null);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<CartDto> AddItemAsync(Guid cartId, Guid productId, int quantity = 1)
    {
        var response = await http.PostAsync(
            $"api/cart/{cartId}/items?productId={productId}&quantity={quantity}",
            null);

        response.EnsureSuccessStatusCode();
        return await this.RefreshCartAsync(cartId);
    }

    /// <inheritdoc/>
    public async Task<CartDto> RemoveItemAsync(Guid cartId, Guid productId)
    {
        var response = await http.DeleteAsync($"api/cart/{cartId}/items/{productId}");
        response.EnsureSuccessStatusCode();
        return await this.RefreshCartAsync(cartId);
    }

    /// <inheritdoc/>
    public async Task<CartDto> ClearAsync(Guid cartId)
    {
        var response = await http.DeleteAsync($"api/cart/{cartId}/clear");
        response.EnsureSuccessStatusCode();
        return await this.RefreshCartAsync(cartId);
    }

    private async Task<CartDto> RefreshCartAsync(Guid cartId)
    {
        return await this.GetCartAsync(cartId)
               ?? throw new InvalidOperationException("Cart not found after mutation.");
    }
}
