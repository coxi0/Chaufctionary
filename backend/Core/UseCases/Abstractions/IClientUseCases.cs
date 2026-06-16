using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IClientUseCases
{
    IEnumerable<Client> GetAll();
    IEnumerable<Client> Rechercher(string terme);
    Client? GetById(int id);
}
