using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IDemandeUseCases
{
    IEnumerable<DemandeModification> GetToutes();
    IEnumerable<DemandeModification> GetMesDemandes(int utilisateurId);
    DemandeModification Creer(DemandeModification demande);
    bool Supprimer(int id);
}
