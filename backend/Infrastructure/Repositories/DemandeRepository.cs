using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories;

public class DemandeRepository : IDemandeRepository
{
    private readonly string _connectionString;

    public DemandeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Chaîne de connexion introuvable.");
    }

    private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

    private const string SelectBase = @"
        SELECT d.Id, d.ClientId, d.UtilisateurId, d.Message, d.DateCreation,
               c.Nom AS ClientNom,
               c.ConseilAcces AS ConseilActuel,
               CONCAT(u.Prenom, ' ', u.Nom) AS ChauffeurNom
        FROM DemandeModification d
        INNER JOIN Client c ON c.Id = d.ClientId
        INNER JOIN Utilisateur u ON u.Id = d.UtilisateurId";

    public IEnumerable<DemandeModification> GetToutes()
    {
        const string sql = SelectBase + " ORDER BY d.DateCreation DESC;";
        using var connection = CreateConnection();
        return connection.Query<DemandeModification>(sql);
    }

    public IEnumerable<DemandeModification> GetParUtilisateur(int utilisateurId)
    {
        const string sql = SelectBase + " WHERE d.UtilisateurId = @UtilisateurId ORDER BY d.DateCreation DESC;";
        using var connection = CreateConnection();
        return connection.Query<DemandeModification>(sql, new { UtilisateurId = utilisateurId });
    }

    public DemandeModification Creer(DemandeModification demande)
    {
        const string sql = @"INSERT INTO DemandeModification (ClientId, UtilisateurId, Message)
                             VALUES (@ClientId, @UtilisateurId, @Message);
                             SELECT LAST_INSERT_ID();";
        using var connection = CreateConnection();
        var nouvelId = connection.QuerySingle<int>(sql, demande);
        demande.Id = nouvelId;
        return demande;
    }

    public bool Supprimer(int id)
    {
        const string sql = "DELETE FROM DemandeModification WHERE Id = @Id;";
        using var connection = CreateConnection();
        var lignes = connection.Execute(sql, new { Id = id });
        return lignes > 0;
    }
}
