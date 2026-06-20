using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IFavoriUseCases
{
    IEnumerable<Client> GetFavoris(int utilisateurId);
    bool Ajouter(int utilisateurId, int clientId);
    bool Supprimer(int utilisateurId, int clientId);
}
