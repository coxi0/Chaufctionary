using Core.Models;
using Core.IGateways;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class UserUseCases : IUserUseCases
{
    private readonly IUserGateway _userGateway;

    public UserUseCases(IUserGateway userGateway)
    {
        _userGateway = userGateway;
    }

    public Utilisateur? Login(string email, string motDePasse)
    {
        var utilisateur = _userGateway.GetByEmail(email);

        if (utilisateur is null)
            return null;

        if (!utilisateur.EstActif)
            return null;

        bool motDePasseCorrect = BCrypt.Net.BCrypt.Verify(motDePasse, utilisateur.MotDePasse);
        if (!motDePasseCorrect)
            return null;

        return utilisateur;
    }

    public IEnumerable<Utilisateur> GetTous()
    {
        return _userGateway.GetAll();
    }

    public Utilisateur? GetById(int id)
    {
        return _userGateway.GetById(id);
    }

    public void CreerUtilisateur(Utilisateur utilisateur, string? roleGestionnaire)
    {
        if (!RolesGerables(roleGestionnaire).Contains(utilisateur.RoleId))
            throw new ArgumentException("Vous n'avez pas le droit de créer ce rôle.");

        utilisateur.MotDePasse = BCrypt.Net.BCrypt.HashPassword(utilisateur.MotDePasse);
        utilisateur.EstActif = true;
        _userGateway.Add(utilisateur);
    }

    public bool ModifierUtilisateur(Utilisateur utilisateur, string? roleGestionnaire)
    {
        var existant = _userGateway.GetById(utilisateur.Id);
        if (existant is null)
            return false;

        var gerables = RolesGerables(roleGestionnaire);
        if (!gerables.Contains(existant.RoleId) || !gerables.Contains(utilisateur.RoleId))
            throw new ArgumentException("Vous n'avez pas le droit de gérer ce rôle.");

        _userGateway.Update(utilisateur);
        return true;
    }

    public bool SupprimerUtilisateur(int id, string? roleGestionnaire)
    {
        var existant = _userGateway.GetById(id);
        if (existant is null)
            return false;

        if (!RolesGerables(roleGestionnaire).Contains(existant.RoleId))
            throw new ArgumentException("Vous n'avez pas le droit de gérer ce rôle.");

        return _userGateway.Delete(id);
    }

    private static int[] RolesGerables(string? role) => role switch
    {
        "Admin" => new[] { 1, 2 },
        "Planneur" => new[] { 1 },
        _ => Array.Empty<int>()
    };
}
