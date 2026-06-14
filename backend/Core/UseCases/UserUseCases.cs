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

    public void Register(Utilisateur utilisateur)
    {
        utilisateur.MotDePasse = BCrypt.Net.BCrypt.HashPassword(utilisateur.MotDePasse);
        _userGateway.Add(utilisateur);
    }
}
