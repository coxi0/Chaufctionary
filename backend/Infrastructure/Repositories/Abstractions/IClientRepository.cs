using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IClientRepository
{
    IEnumerable<Client> GetAll();
    IEnumerable<Client> Rechercher(string terme);
    Client? GetById(int id);
    Client? GetByNumero(string numero);

    Client Creer(Client client);
    Client? Modifier(Client client);
    bool Supprimer(int id);
}
