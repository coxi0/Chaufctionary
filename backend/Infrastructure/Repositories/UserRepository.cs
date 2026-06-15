using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Chaîne de connexion introuvable.");
    }

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

    public Utilisateur? GetByEmail(string email)
    {
        const string sql = @"SELECT u.Id, u.Nom, u.Prenom, u.Email, u.MotDePasse,
                                    u.EstActif, u.RoleId, r.Libelle AS Role
                             FROM Utilisateur u
                             INNER JOIN Role r ON u.RoleId = r.Id
                             WHERE u.Email = @Email;";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<Utilisateur>(sql, new { Email = email });
    }
    public void Add(Utilisateur user)
    {
        const string sql = @"INSERT INTO Utilisateur (Nom, Prenom, Email, MotDePasse, EstActif, RoleId)
                             VALUES (@Nom, @Prenom, @Email, @MotDePasse, @EstActif, @RoleId);";
        using var connection = CreateConnection();
        connection.Execute(sql, user);
    }


}
