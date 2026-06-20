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
        const string sql = "SELECT Id, Numero, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude, Notes, ConseilAcces FROM Client;";
        using var connection = CreateConnection();
        return connection.Query<Client>(sql);
    }

    public IEnumerable<Client> Rechercher(string terme)
    {
        const string sql = @"SELECT Id, Numero, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude, Notes, ConseilAcces
                             FROM Client
                             WHERE Nom LIKE @Terme OR Ville LIKE @Terme;";
        using var connection = CreateConnection();
        return connection.Query<Client>(sql, new { Terme = $"%{terme}%" });
    }

    public Client? GetById(int id)
    {
        const string sql = "SELECT Id, Numero, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude, Notes, ConseilAcces FROM Client WHERE Id = @Id;";
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<Client>(sql, new { Id = id });
    }


    public Client Creer(Client client)
    {
        const string sql = @"INSERT INTO Client (Numero, Nom, Adresse, Ville, CodePostal, Telephone, Latitude, Longitude, Notes, ConseilAcces)
                             VALUES (@Numero, @Nom, @Adresse, @Ville, @CodePostal, @Telephone, @Latitude, @Longitude, @Notes, @ConseilAcces);
                             SELECT LAST_INSERT_ID();";
        using var connection = CreateConnection();
        var nouvelId = connection.QuerySingle<int>(sql, client);
        client.Id = nouvelId;
        return client;
    }

    public Client? Modifier(Client client)
    {
        const string sql = @"UPDATE Client
                             SET Numero = @Numero,
                                 Nom = @Nom,
                                 Adresse = @Adresse,
                                 Ville = @Ville,
                                 CodePostal = @CodePostal,
                                 Telephone = @Telephone,
                                 Latitude = @Latitude,
                                 Longitude = @Longitude,
                                 Notes = @Notes,
                                 ConseilAcces = @ConseilAcces
                             WHERE Id = @Id;";
        using var connection = CreateConnection();
        var lignesModifiees = connection.Execute(sql, client);
        return lignesModifiees > 0 ? client : null;
    }

    public bool Supprimer(int id)
    {
        const string sql = "DELETE FROM Client WHERE Id = @Id;";
        using var connection = CreateConnection();
        var lignesSupprimees = connection.Execute(sql, new { Id = id });
        return lignesSupprimees > 0;
    }
}
