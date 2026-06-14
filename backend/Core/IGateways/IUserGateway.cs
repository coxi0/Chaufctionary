using Core.Models;
namespace Core.IGateways;

public interface IUserGateway{
    Utilisateur? GetByEmail(string email);
    void Add(Utilisateur user);


}