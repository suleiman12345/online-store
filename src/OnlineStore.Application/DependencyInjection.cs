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
        services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IUserSessionService, UserSessionService>();

        return services;
    }
}
