// <copyright file="IOrderItemRepository.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Application.Interfaces.Repositories;

using OnlineStore.Domain.Entities;

/// <summary>
/// Репозиторий для элементов заказа.
/// </summary>
public interface IOrderItemRepository : IRepository<OrderItem>
{
}
