// <copyright file="CartService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Services;

using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Contracts.DTOs;
using OnlineStore.Domain.Entities;

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
        this._cartRepository = cartRepository;
        this._productRepository = productRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(CancellationToken cancellationToken = default)
    {
        var entity = new Cart
        {
            Id = Guid.NewGuid(),
            Items = [],
        };

        await this._cartRepository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<CartDto?> GetAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken);

        return cart is null ? null : MapToDto(cart);
    }

    /// <inheritdoc/>
    public Task AddItemAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken = default)
        => this.AddProductAsync(cartId, productId, quantity, cancellationToken);

    /// <inheritdoc/>
    public Task RemoveItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default)
        => this.RemoveProductAsync(cartId, productId, cancellationToken);

    /// <inheritdoc/>
    public async Task ClearAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        cart.Items.Clear();

        this._cartRepository.Update(cart);
        await this._cartRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task AddProductAsync(
        Guid cartId,
        Guid productId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken);

        if (cart is null)
        {
            cart = new Cart
            {
                Id = cartId,
                Items = [],
            };

            await this._cartRepository.AddAsync(cart, cancellationToken);
        }

        var product = await this._productRepository.GetByIdAsync(productId, cancellationToken)
            ?? throw new KeyNotFoundException("Product not found");

        var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            await this._cartRepository.AddItemAsync(new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price,
            }, cancellationToken);
        }

        await this._cartRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveProductAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
            ?? throw new KeyNotFoundException("Cart not found");

        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
        {
            return;
        }

        cart.Items.Remove(item);

        this._cartRepository.Update(cart);
        await this._cartRepository.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> GetItemCountAsync(Guid cartId, CancellationToken cancellationToken = default)
    {
        var cart = await this._cartRepository.GetCartWithItemsAsync(cartId, cancellationToken)
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
