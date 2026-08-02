using Lateral.Application.Dashboard;
using Lateral.Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Lateral.Application;

/// <summary>
/// Registro de dependencias de la capa Application.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
