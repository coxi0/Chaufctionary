using Core.Models;
namespace Core.IGateways;

public interface IUserGateway{
    Utilisateur? GetByEmail(string email);
    IEnumerable<Utilisateur> GetAll();
    Utilisateur? GetById(int id);
    void Add(Utilisateur user);
    void Update(Utilisateur user);
    bool Delete(int id);
}