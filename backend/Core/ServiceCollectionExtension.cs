using Core.UseCases;
using Core.UseCases.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IUserUseCases, UserUseCases>();
        services.AddTransient<IClientUseCases, ClientUseCases>();
        return services;
    }
}
