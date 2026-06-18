using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ClientUseCases : IClientUseCases
{
    private readonly IClientGateway _clientGateway;

    public ClientUseCases(IClientGateway clientGateway)
    {
        _clientGateway = clientGateway;
    }

    public IEnumerable<Client> GetAll()
    {
        return _clientGateway.GetAll();
    }

    public IEnumerable<Client> Rechercher(string terme)
    {
        if (string.IsNullOrWhiteSpace(terme))
            return _clientGateway.GetAll();

        return _clientGateway.Rechercher(terme);
    }

    public Client? GetById(int id)
    {
        return _clientGateway.GetById(id);
    }

    public Client Creer(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.Nom))
            throw new ArgumentException("Le nom du client est obligatoire.");

        return _clientGateway.Creer(client);
    }

    public Client? Modifier(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.Nom))
            throw new ArgumentException("Le nom du client est obligatoire.");

        var existant = _clientGateway.GetById(client.Id);
        if (existant is null)
            return null;

        return _clientGateway.Modifier(client);
    }

    public bool Supprimer(int id)
    {
        return _clientGateway.Supprimer(id);
    }
}
