using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUserUseCases
{
    Utilisateur? Login(string email, string motDePasse);
    IEnumerable<Utilisateur> GetTous();
    Utilisateur? GetById(int id);
    void CreerUtilisateur(Utilisateur utilisateur, string? roleGestionnaire);
    bool ModifierUtilisateur(Utilisateur utilisateur, string? roleGestionnaire);
    bool SupprimerUtilisateur(int id, string? roleGestionnaire);
}
