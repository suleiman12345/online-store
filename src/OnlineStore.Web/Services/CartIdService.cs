using Microsoft.JSInterop;
using OnlineStore.Contracts;

namespace OnlineStore.Web.Services;

public class CartIdService(IJSRuntime jsRuntime, IConfiguration configuration) : ICartIdService
{
    private const string StorageKey = "cartId";
    private Guid? _cartId;

    public async Task EnsureInitializedAsync()
    {
        _ = await GetCartIdAsync();
    }

    public async Task<Guid> GetCartIdAsync()
    {
        if (_cartId.HasValue)
        {
            return _cartId.Value;
        }

        var stored = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (Guid.TryParse(stored, out var parsed))
        {
            _cartId = parsed;
            return parsed;
        }

        var defaultCartId = configuration["Cart:DefaultCartId"];

        if (!Guid.TryParse(defaultCartId, out var cartId))
        {
            cartId = StoreDefaults.DefaultCartId;
        }

        await jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, cartId.ToString());
        _cartId = cartId;
        return cartId;
    }
}
