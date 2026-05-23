// <copyright file="ICartItemRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для элементов корзины.
/// </summary>
public interface ICartItemRepository : IRepository<CartItem>
{
}
