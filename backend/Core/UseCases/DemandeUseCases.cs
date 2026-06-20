using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class DemandeUseCases : IDemandeUseCases
{
    private readonly IDemandeGateway _demandeGateway;

    public DemandeUseCases(IDemandeGateway demandeGateway)
    {
        _demandeGateway = demandeGateway;
    }

    public IEnumerable<DemandeModification> GetToutes()
    {
        return _demandeGateway.GetToutes();
    }

    public IEnumerable<DemandeModification> GetMesDemandes(int utilisateurId)
    {
        return _demandeGateway.GetParUtilisateur(utilisateurId);
    }

    public DemandeModification Creer(DemandeModification demande)
    {
        if (string.IsNullOrWhiteSpace(demande.Message))
            throw new ArgumentException("La proposition d'accès est obligatoire.");

        return _demandeGateway.Creer(demande);
    }

    public bool Supprimer(int id)
    {
        return _demandeGateway.Supprimer(id);
    }
}
