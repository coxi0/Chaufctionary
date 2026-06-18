using Core.Models;

namespace Core.IGateways;

public interface IClientGateway
{
    IEnumerable<Client> GetAll();
    IEnumerable<Client> Rechercher(string terme);
    Client? GetById(int id);

    Client Creer(Client client);
    Client? Modifier(Client client);
    bool Supprimer(int id);
}
