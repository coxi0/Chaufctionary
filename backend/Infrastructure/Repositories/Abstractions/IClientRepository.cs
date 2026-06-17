using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IClientRepository
{
    IEnumerable<Client> GetAll();
    IEnumerable<Client> Rechercher(string terme);
    Client? GetById(int id);

    Client? Modifier(Client client);
}
