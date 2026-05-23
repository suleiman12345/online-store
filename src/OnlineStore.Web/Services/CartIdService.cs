// <copyright file="CartIdService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using Microsoft.JSInterop;

public class CartIdService(
    IJSRuntime jsRuntime,
    ICartApiService cartApiService) : ICartIdService
{
    private const string StorageKey = "cartId";
    private Guid? _cartId;

    /// <inheritdoc/>
    public async Task EnsureInitializedAsync()
    {
        _ = await this.GetCartIdAsync();
    }

    /// <inheritdoc/>
    public async Task<Guid> GetCartIdAsync()
    {
        if (this._cartId.HasValue)
        {
            return this._cartId.Value;
        }

        var stored = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (Guid.TryParse(stored, out var parsed))
        {
            this._cartId = parsed;
            return parsed;
        }

        var newCartId = await cartApiService.CreateCartAsync();

        await jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, newCartId.ToString());
        this._cartId = newCartId;

        return newCartId;
    }
}
