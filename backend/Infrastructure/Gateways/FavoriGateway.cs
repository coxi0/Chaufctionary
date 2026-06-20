using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class FavoriGateway : IFavoriGateway
{
    private readonly IFavoriRepository _favoriRepository;

    public FavoriGateway(IFavoriRepository favoriRepository)
    {
        _favoriRepository = favoriRepository;
    }

    public IEnumerable<Core.Models.Client> GetFavoris(int utilisateurId)
    {
        return _favoriRepository.GetFavoris(utilisateurId).Select(Map);
    }

    public bool Ajouter(int utilisateurId, int clientId)
    {
        return _favoriRepository.Ajouter(utilisateurId, clientId);
    }

    public bool Supprimer(int utilisateurId, int clientId)
    {
        return _favoriRepository.Supprimer(utilisateurId, clientId);
    }

    private static Core.Models.Client Map(Infrastructure.Models.Client c)
    {
        return new Core.Models.Client
        {
            Id = c.Id,
            Numero = c.Numero,
            Nom = c.Nom,
            Adresse = c.Adresse,
            Ville = c.Ville,
            CodePostal = c.CodePostal,
            Telephone = c.Telephone,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            Notes = c.Notes
        };
    }
}
