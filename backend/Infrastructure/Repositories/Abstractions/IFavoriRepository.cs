using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IFavoriRepository
{
    IEnumerable<Client> GetFavoris(int utilisateurId);
    bool Ajouter(int utilisateurId, int clientId);
    bool Supprimer(int utilisateurId, int clientId);
}
