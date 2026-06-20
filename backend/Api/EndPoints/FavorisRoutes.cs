using System.Security.Claims;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class FavorisRoutes
{
    public static WebApplication AddFavorisRoutes(this WebApplication app)
    {
        // Tout utilisateur connecté gère SES propres favoris (acteur principal : le chauffeur).
        var group = app.MapGroup("api/favoris")
            .RequireAuthorization()
            .WithTags("Favoris");

        group.MapGet("", (IFavoriUseCases favoriUseCases, HttpContext httpContext) =>
        {
            var utilisateurId = GetUtilisateurId(httpContext);
            var favoris = favoriUseCases.GetFavoris(utilisateurId);
            return Results.Ok(favoris);
        });

        group.MapPost("{clientId:int}", (int clientId, IFavoriUseCases favoriUseCases, HttpContext httpContext) =>
        {
            var utilisateurId = GetUtilisateurId(httpContext);
            favoriUseCases.Ajouter(utilisateurId, clientId);
            return Results.NoContent();
        });

        group.MapDelete("{clientId:int}", (int clientId, IFavoriUseCases favoriUseCases, HttpContext httpContext) =>
        {
            var utilisateurId = GetUtilisateurId(httpContext);
            var ok = favoriUseCases.Supprimer(utilisateurId, clientId);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }

    // Lit l'identifiant de l'utilisateur connecté depuis le JWT (claim NameIdentifier).
    private static int GetUtilisateurId(HttpContext httpContext)
    {
        var valeur = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(valeur!);
    }
}
