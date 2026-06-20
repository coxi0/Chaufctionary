using Core.Models;

namespace Core.IGateways;

public interface IFavoriGateway
{
    IEnumerable<Client> GetFavoris(int utilisateurId);
    bool Ajouter(int utilisateurId, int clientId);
    bool Supprimer(int utilisateurId, int clientId);
}
