// <copyright file="IProductApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using OnlineStore.Contracts.DTOs;

public interface IProductApiService
{
    Task<IReadOnlyList<ProductDto>> GetAllAsync();

    Task<IReadOnlyList<ProductDto>> GetByCategoryAsync(Guid categoryId);

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(ProductCreateDto dto);
}
