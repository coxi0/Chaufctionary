using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class ClientRoutes
{
    public static WebApplication AddClientRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/clients")
            .RequireAuthorization()
            .WithTags("Clients");

        group.MapGet("", (string? recherche, IClientUseCases clientUseCases) =>
        {
            var clients = clientUseCases.Rechercher(recherche ?? "");
            return Results.Ok(clients);
        });

        group.MapGet("{id:int}", (int id, IClientUseCases clientUseCases) =>
        {
            var client = clientUseCases.GetById(id);
            return client is null ? Results.NotFound() : Results.Ok(client);
        });

        group.MapPost("", (Client client, IClientUseCases clientUseCases) =>
        {
            try
            {
                var cree = clientUseCases.Creer(client);
                return Results.Created($"/api/clients/{cree.Id}", cree);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { erreur = ex.Message });
            }
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        group.MapPut("{id:int}", (int id, Client client, IClientUseCases clientUseCases) =>
        {
            client.Id = id; 
            try
            {
                var modifie = clientUseCases.Modifier(client);
                return modifie is null ? Results.NotFound() : Results.Ok(modifie);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { erreur = ex.Message });
            }
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        group.MapDelete("{id:int}", (int id, IClientUseCases clientUseCases) =>
        {
            var supprime = clientUseCases.Supprimer(id);
            return supprime ? Results.NoContent() : Results.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        return app;
    }
}
