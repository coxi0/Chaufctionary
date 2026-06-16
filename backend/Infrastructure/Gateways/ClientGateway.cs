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
