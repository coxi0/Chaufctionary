using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ClientGateway : IClientGateway
{
    private readonly IClientRepository _clientRepository;

    public ClientGateway(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public IEnumerable<Core.Models.Client> GetAll()
    {
        return _clientRepository.GetAll().Select(Map);
    }

    public IEnumerable<Core.Models.Client> Rechercher(string terme)
    {
        return _clientRepository.Rechercher(terme).Select(Map);
    }

    public Core.Models.Client? GetById(int id)
    {
        var infra = _clientRepository.GetById(id);
        return infra is null ? null : Map(infra);
    }

    public Core.Models.Client? Modifier(Core.Models.Client client)
    {
        // 1. Traduire Core → Infra (la donnée descend vers la BDD).
        var infra = MapToInfra(client);

        // 2. Appeler le repository, qui fait le UPDATE Dapper.
        var resultat = _clientRepository.Modifier(infra);

        // 3. Retraduire Infra → Core (la donnée remonte) ; null si rien modifié.
        return resultat is null ? null : Map(resultat);
    }

    private static Infrastructure.Models.Client MapToInfra(Core.Models.Client c)
    {
        return new Infrastructure.Models.Client
        {
            Id = c.Id,
            Nom = c.Nom,
            Adresse = c.Adresse,
            Ville = c.Ville,
            CodePostal = c.CodePostal,
            Telephone = c.Telephone,
            Latitude = c.Latitude,
            Longitude = c.Longitude
        };
    }

    private static Core.Models.Client Map(Infrastructure.Models.Client c)
    {
        return new Core.Models.Client
        {
            Id = c.Id,
            Nom = c.Nom,
            Adresse = c.Adresse,
            Ville = c.Ville,
            CodePostal = c.CodePostal,
            Telephone = c.Telephone,
            Latitude = c.Latitude,
            Longitude = c.Longitude
        };
    }
}
