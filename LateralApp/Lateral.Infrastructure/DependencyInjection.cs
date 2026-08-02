using Lateral.Domain.Entities;
using Lateral.Domain.Interfaces;
using Lateral.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Lateral.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa Infrastructure.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();

        return services;
    }
}
