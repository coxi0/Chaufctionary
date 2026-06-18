using Infrastructure.Models;
namespace Infrastructure.Repositories.Abstractions;
public interface IUserRepository{
    Utilisateur? GetByEmail(string email);
    IEnumerable<Utilisateur> GetAll();
    Utilisateur? GetById(int id);
    void Add(Utilisateur user);
    void Update(Utilisateur user);
    bool Delete(int id);
}