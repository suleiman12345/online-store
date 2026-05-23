// <copyright file="ICartRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для работы с корзинами пользователей.
/// </summary>
public interface ICartRepository : IRepository<Cart>
{
    /// <summary>
    /// Создание корзины.
    /// </summary>
    Task<Guid> CreateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает корзину вместе с товарами.
    /// </summary>
    Task<Cart?> GetCartWithItemsAsync(Guid cartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет позицию в корзину.
    /// </summary>
    Task AddItemAsync(CartItem item, CancellationToken cancellationToken = default);
}
