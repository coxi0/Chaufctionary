using Core.Models;

namespace Core.IGateways;

public interface IDemandeGateway
{
    IEnumerable<DemandeModification> GetToutes();
    IEnumerable<DemandeModification> GetParUtilisateur(int utilisateurId);
    DemandeModification Creer(DemandeModification demande);
    bool Supprimer(int id);
}
