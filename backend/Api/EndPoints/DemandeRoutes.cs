using System.Security.Claims;
using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class DemandeRoutes
{
    public static WebApplication AddDemandeRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/demandes")
            .RequireAuthorization()
            .WithTags("Demandes");

        group.MapPost("", (DemandeRequest request, IDemandeUseCases demandeUseCases, HttpContext httpContext) =>
        {
            var demande = new DemandeModification
            {
                ClientId = request.ClientId,
                UtilisateurId = GetUtilisateurId(httpContext),
                Message = request.Message
            };

            try
            {
                var creee = demandeUseCases.Creer(demande);
                return Results.Created($"/api/demandes/{creee.Id}", creee);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { erreur = ex.Message });
            }
        });

        group.MapGet("mes", (IDemandeUseCases demandeUseCases, HttpContext httpContext) =>
        {
            var demandes = demandeUseCases.GetMesDemandes(GetUtilisateurId(httpContext));
            return Results.Ok(demandes);
        });

        // Le planneur/admin liste toutes les demandes à traiter.
        group.MapGet("", (IDemandeUseCases demandeUseCases) =>
        {
            return Results.Ok(demandeUseCases.GetToutes());
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        group.MapDelete("{id:int}", (int id, IDemandeUseCases demandeUseCases) =>
        {
            var ok = demandeUseCases.Supprimer(id);
            return ok ? Results.NoContent() : Results.NotFound();
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        return app;
    }

    private static int GetUtilisateurId(HttpContext httpContext)
    {
        var valeur = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.Parse(valeur!);
    }
}
