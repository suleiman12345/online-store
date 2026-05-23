using OnlineStore.Contracts.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

/// <summary>
/// Сервис работы с корзиной.
/// </summary>
public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<CartDto?> GetAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken);

        if (cart is null)
        {
            return null;
        }

        return MapToDto(cart);
    }

    public Task AddItemAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        => AddProductAsync(cartId, productId, quantity, cancellationToken);

    public Task RemoveItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default)
        => RemoveProductAsync(cartId, productId, cancellationToken);

    public async Task ClearAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        cart.Items.Clear();

        _cartRepository.Update(cart);
        await _cartRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task AddProductAsync(
        Guid cartId,
        Guid productId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken);

        if (cart is null)
        {
            cart = new Cart
            {
                Id = cartId,
                Items = [],
            };

            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken)
            ?? throw new KeyNotFoundException("Product not found");

        var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            await _cartRepository.AddItemAsync(new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price,
            }, cancellationToken);
        }

        await _cartRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveProductAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
        {
            return;
        }

        cart.Items.Remove(item);

        _cartRepository.Update(cart);
        await _cartRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetItemCountAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        return cart.Items.Sum(x => x.Quantity);
    }

    private static CartDto MapToDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            Items = cart.Items.Select(item => new CartItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product?.Name ?? string.Empty,
                Price = item.UnitPrice > 0 ? item.UnitPrice : item.Product?.Price ?? 0,
                Quantity = item.Quantity,
            }).ToList(),
        };
    }
}
