using Core.IGateways;
using Infrastructure.Gateways;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserGateway, UserGateway>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IClientGateway, ClientGateway>();
        services.AddTransient<IFavoriRepository, FavoriRepository>();
        services.AddTransient<IFavoriGateway, FavoriGateway>();
        services.AddTransient<IDemandeRepository, DemandeRepository>();
        services.AddTransient<IDemandeGateway, DemandeGateway>();
        return services;
    }
}
