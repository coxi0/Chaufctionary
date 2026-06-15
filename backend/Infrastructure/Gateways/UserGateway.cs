using Core.IGateways;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class UserGateway : IUserGateway
{
    private readonly IUserRepository _userRepository;

    public UserGateway(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Core.Models.Utilisateur? GetByEmail(string email)
    {
        var infra = _userRepository.GetByEmail(email);
        if (infra is null)
            return null;

        return new Core.Models.Utilisateur
        {
            Id = infra.Id,
            Nom = infra.Nom,
            Prenom = infra.Prenom,
            Email = infra.Email,
            MotDePasse = infra.MotDePasse,
            EstActif = infra.EstActif,
            RoleId = infra.RoleId,
            Role = infra.Role
        };
    }

    public void Add(Core.Models.Utilisateur user)
    {
        var infra = new Infrastructure.Models.Utilisateur
        {
            Nom = user.Nom,
            Prenom = user.Prenom,
            Email = user.Email,
            MotDePasse = user.MotDePasse,
            EstActif = user.EstActif,
            RoleId = user.RoleId
        };
        _userRepository.Add(infra);
    }
}
