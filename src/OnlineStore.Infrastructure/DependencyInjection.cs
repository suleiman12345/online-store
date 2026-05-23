// <copyright file="DependencyInjection.cs" company="OnlineStore">
// Copyright (c) OnlineStore. All rights reserved.
// </copyright>

namespace OnlineStore.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Repositories;

/// <summary>
/// Infrastructure layer dependency injection extensions.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует инфраструктуру: DbContext и репозитории.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Generic repository
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        // Specific repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
