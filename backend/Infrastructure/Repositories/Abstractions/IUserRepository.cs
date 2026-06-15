using Infrastructure.Models;
namespace Infrastructure.Repositories.Abstractions;
public interface IUserRepository{
    Utilisateur? GetByEmail(string email);
    void Add(Utilisateur user);


}