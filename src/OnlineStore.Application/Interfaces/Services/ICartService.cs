// <copyright file="ICartService.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Services;

using OnlineStore.Contracts.DTOs;

/// <summary>
/// Сервис работы с корзиной.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Создание корзины.
    /// </summary>
    Task<Guid> CreateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает корзину пользователя.
    /// </summary>
    Task<CartDto?> GetAsync(Guid cartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет товар в корзину.
    /// </summary>
    Task AddItemAsync(Guid cartId, Guid productId, int quantity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет товар из корзины.
    /// </summary>
    Task RemoveItemAsync(Guid cartId, Guid productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Очищает корзину.
    /// </summary>
    Task ClearAsync(Guid cartId, CancellationToken cancellationToken = default);

     /// <summary>
    /// Количество товаров в корзине (сумма Quantity).
    /// </summary>
    Task<int> GetItemCountAsync(Guid cartId, CancellationToken cancellationToken = default);
}
