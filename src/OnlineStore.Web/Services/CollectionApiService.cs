// <copyright file="CollectionApiService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Web.Services;

using System.Net.Http.Json;
using OnlineStore.Contracts.DTOs;

public class CollectionApiService(HttpClient http) : ICollectionApiService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        return await http.GetFromJsonAsync<List<CategoryDto>>("api/collections")
               ?? [];
    }
}
