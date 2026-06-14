using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IUserUseCases
{
    Utilisateur? Login(string email, string motDePasse);
    void Register(Utilisateur utilisateur);
}
