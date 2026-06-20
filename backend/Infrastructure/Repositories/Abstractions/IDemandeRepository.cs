using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface IDemandeRepository
{
    IEnumerable<DemandeModification> GetToutes();
    IEnumerable<DemandeModification> GetParUtilisateur(int utilisateurId);
    DemandeModification Creer(DemandeModification demande);
    bool Supprimer(int id);
}
