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

        return app;
    }
}
