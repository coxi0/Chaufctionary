using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Chaîne de connexion introuvable.");
    }

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

    public IEnumerable<Client> GetAll()
    {
        const string sql = "SELECT Id, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude FROM Client;";
        using var connection = CreateConnection();
        return connection.Query<Client>(sql);
    }

    public IEnumerable<Client> Rechercher(string terme)
    {
        const string sql = @"SELECT Id, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude
                             FROM Client
                             WHERE Nom LIKE @Terme OR Ville LIKE @Terme;";
        using var connection = CreateConnection();
        return connection.Query<Client>(sql, new { Terme = $"%{terme}%" });
    }

    public Client? GetById(int id)
    {
        const string sql = "SELECT Id, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude FROM Client WHERE Id = @Id;";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<Client>(sql, new { Id = id });
    }
}
