// <copyright file="ICartApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using OnlineStore.Contracts.DTOs;

public interface ICartApiService
{
    Task<Guid> CreateCartAsync();

    Task<CartDto?> GetCartAsync(Guid cartId);

    Task<CartDto> AddItemAsync(Guid cartId, Guid productId, int quantity = 1);

    Task<CartDto> RemoveItemAsync(Guid cartId, Guid productId);

    Task<CartDto> ClearAsync(Guid cartId);
}
