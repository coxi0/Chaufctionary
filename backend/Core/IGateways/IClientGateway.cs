using Core.Models;

namespace Core.IGateways;

public interface IClientGateway
{
    IEnumerable<Client> GetAll();
    IEnumerable<Client> Rechercher(string terme);
    Client? GetById(int id);
}
