using Microsoft.Extensions.DependencyInjection;

namespace Lateral.Application;

/// <summary>
/// Registro de dependencias de la capa Application.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar casos de uso aquí
        return services;
    }
}
