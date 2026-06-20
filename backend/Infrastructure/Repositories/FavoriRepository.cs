using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class FavoriRepository : IFavoriRepository
{
    private readonly string _connectionString;

    public FavoriRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Chaîne de connexion introuvable.");
    }

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

    public IEnumerable<Client> GetFavoris(int utilisateurId)
    {
        const string sql = @"SELECT c.Id, c.Numero, c.Nom, c.Adresse, c.Ville, c.CodePostal, c.Telephone, c.Latitude, c.Longitude, c.Notes
                             FROM Client c
                             INNER JOIN Favori f ON f.ClientId = c.Id
                             WHERE f.UtilisateurId = @UtilisateurId;";
        using var connection = CreateConnection();
        return connection.Query<Client>(sql, new { UtilisateurId = utilisateurId });
    }

    public bool Ajouter(int utilisateurId, int clientId)
    {
        // INSERT IGNORE : si le favori existe déjà (clé primaire composite), on ne lève pas d'erreur.
        const string sql = @"INSERT IGNORE INTO Favori (UtilisateurId, ClientId)
                             VALUES (@UtilisateurId, @ClientId);";
        using var connection = CreateConnection();
        var lignes = connection.Execute(sql, new { UtilisateurId = utilisateurId, ClientId = clientId });
        return lignes > 0;
    }

    public bool Supprimer(int utilisateurId, int clientId)
    {
        const string sql = @"DELETE FROM Favori
                             WHERE UtilisateurId = @UtilisateurId AND ClientId = @ClientId;";
        using var connection = CreateConnection();
        var lignes = connection.Execute(sql, new { UtilisateurId = utilisateurId, ClientId = clientId });
        return lignes > 0;
    }
}
