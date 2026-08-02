using Microsoft.Extensions.DependencyInjection;

namespace Lateral.Infrastructure;

/// <summary>
/// Registro de dependencias de la capa Infrastructure.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Registrar repositorios, DbContext, servicios externos, etc.
        return services;
    }
}
