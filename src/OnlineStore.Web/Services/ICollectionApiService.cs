// <copyright file="ICollectionApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using OnlineStore.Contracts.DTOs;

public interface ICollectionApiService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
}
