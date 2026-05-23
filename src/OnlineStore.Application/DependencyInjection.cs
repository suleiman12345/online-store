using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Application.Services;
using OnlineStore.Application.Validators;

namespace OnlineStore.Application;

/// <summary>
/// Application layer dependency injection extensions.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services and validators.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ICartService, CartService>();

    return services;
}
}
