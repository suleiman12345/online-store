// <copyright file="IOrderApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using OnlineStore.Contracts.DTOs;

public interface IOrderApiService
{
    Task<IReadOnlyList<OrderDto>> GetAllAsync();

    Task<OrderDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateFromCartAsync(Guid cartId);
}
