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
        return infra is null ? null : Map(infra);
    }

    public IEnumerable<Core.Models.Utilisateur> GetAll()
    {
        return _userRepository.GetAll().Select(Map);
    }

    public Core.Models.Utilisateur? GetById(int id)
    {
        var infra = _userRepository.GetById(id);
        return infra is null ? null : Map(infra);
    }

    public void Add(Core.Models.Utilisateur user)
    {
        _userRepository.Add(MapToInfra(user));
    }

    public void Update(Core.Models.Utilisateur user)
    {
        _userRepository.Update(MapToInfra(user));
    }

    public bool Delete(int id)
    {
        return _userRepository.Delete(id);
    }

    private static Core.Models.Utilisateur Map(Infrastructure.Models.Utilisateur u)
    {
        return new Core.Models.Utilisateur
        {
            Id = u.Id,
            Nom = u.Nom,
            Prenom = u.Prenom,
            Email = u.Email,
            MotDePasse = u.MotDePasse,
            EstActif = u.EstActif,
            RoleId = u.RoleId,
            Role = u.Role
        };
    }

    private static Infrastructure.Models.Utilisateur MapToInfra(Core.Models.Utilisateur u)
    {
        return new Infrastructure.Models.Utilisateur
        {
            Id = u.Id,
            Nom = u.Nom,
            Prenom = u.Prenom,
            Email = u.Email,
            MotDePasse = u.MotDePasse,
            EstActif = u.EstActif,
            RoleId = u.RoleId
        };
    }
}
