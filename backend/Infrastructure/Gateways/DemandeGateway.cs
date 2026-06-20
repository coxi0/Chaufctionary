using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class DemandeGateway : IDemandeGateway
{
    private readonly IDemandeRepository _demandeRepository;

    public DemandeGateway(IDemandeRepository demandeRepository)
    {
        _demandeRepository = demandeRepository;
    }

    public IEnumerable<Core.Models.DemandeModification> GetToutes()
    {
        return _demandeRepository.GetToutes().Select(Map);
    }

    public IEnumerable<Core.Models.DemandeModification> GetParUtilisateur(int utilisateurId)
    {
        return _demandeRepository.GetParUtilisateur(utilisateurId).Select(Map);
    }

    public Core.Models.DemandeModification Creer(Core.Models.DemandeModification demande)
    {
        var resultat = _demandeRepository.Creer(MapToInfra(demande));
        return Map(resultat);
    }

    public bool Supprimer(int id)
    {
        return _demandeRepository.Supprimer(id);
    }

    private static Infrastructure.Models.DemandeModification MapToInfra(Core.Models.DemandeModification d)
    {
        return new Infrastructure.Models.DemandeModification
        {
            Id = d.Id,
            ClientId = d.ClientId,
            UtilisateurId = d.UtilisateurId,
            Message = d.Message
        };
    }

    private static Core.Models.DemandeModification Map(Infrastructure.Models.DemandeModification d)
    {
        return new Core.Models.DemandeModification
        {
            Id = d.Id,
            ClientId = d.ClientId,
            UtilisateurId = d.UtilisateurId,
            Message = d.Message,
            DateCreation = d.DateCreation,
            ClientNom = d.ClientNom,
            ChauffeurNom = d.ChauffeurNom,
            ConseilActuel = d.ConseilActuel
        };
    }
}
