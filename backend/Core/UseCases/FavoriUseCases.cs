using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class FavoriUseCases : IFavoriUseCases
{
    private readonly IFavoriGateway _favoriGateway;

    public FavoriUseCases(IFavoriGateway favoriGateway)
    {
        _favoriGateway = favoriGateway;
    }

    public IEnumerable<Client> GetFavoris(int utilisateurId)
    {
        return _favoriGateway.GetFavoris(utilisateurId);
    }

    public bool Ajouter(int utilisateurId, int clientId)
    {
        return _favoriGateway.Ajouter(utilisateurId, clientId);
    }

    public bool Supprimer(int utilisateurId, int clientId)
    {
        return _favoriGateway.Supprimer(utilisateurId, clientId);
    }
}
