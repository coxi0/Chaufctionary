using System.Security.Claims;
using Api.Models;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class UtilisateurRoutes
{
    public static WebApplication AddUtilisateurRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/utilisateurs")
            .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"))
            .WithTags("Utilisateurs");

        group.MapGet("", (IUserUseCases userUseCases) =>
        {
            var utilisateurs = userUseCases.GetTous().Select(ToResponse);
            return Results.Ok(utilisateurs);
        });

        group.MapGet("{id:int}", (int id, IUserUseCases userUseCases) =>
        {
            var utilisateur = userUseCases.GetById(id);
            return utilisateur is null ? Results.NotFound() : Results.Ok(ToResponse(utilisateur));
        });

        group.MapPost("", (RegisterRequest request, IUserUseCases userUseCases, HttpContext httpContext) =>
        {
            var roleGestionnaire = httpContext.User.FindFirstValue(ClaimTypes.Role);

            var utilisateur = new Utilisateur
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                MotDePasse = request.MotDePasse,
                RoleId = request.RoleId
            };

            userUseCases.CreerUtilisateur(utilisateur, roleGestionnaire);
            return Results.Created();
        });

        group.MapPut("{id:int}", (int id, ModifierUtilisateurRequest request, IUserUseCases userUseCases, HttpContext httpContext) =>
        {
            var roleGestionnaire = httpContext.User.FindFirstValue(ClaimTypes.Role);

            var utilisateur = new Utilisateur
            {
                Id = id,
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                EstActif = request.EstActif,
                RoleId = request.RoleId
            };

            var ok = userUseCases.ModifierUtilisateur(utilisateur, roleGestionnaire);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        group.MapDelete("{id:int}", (int id, IUserUseCases userUseCases, HttpContext httpContext) =>
        {
            var roleGestionnaire = httpContext.User.FindFirstValue(ClaimTypes.Role);

            var ok = userUseCases.SupprimerUtilisateur(id, roleGestionnaire);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }

    private static UtilisateurResponse ToResponse(Utilisateur u) => new()
    {
        Id = u.Id,
        Nom = u.Nom,
        Prenom = u.Prenom,
        Email = u.Email,
        EstActif = u.EstActif,
        RoleId = u.RoleId,
        Role = u.Role
    };
}
