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
            .RequireAuthorization()
            .WithTags("Utilisateurs");

        group.MapPost("", (RegisterRequest request, IUserUseCases userUseCases, HttpContext httpContext) =>
        {
            var roleCreateur = httpContext.User.FindFirstValue(ClaimTypes.Role);

            var utilisateur = new Utilisateur
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                MotDePasse = request.MotDePasse,
                RoleId = request.RoleId
            };

            try
            {
                userUseCases.CreerUtilisateur(utilisateur, roleCreateur);
                return Results.Created();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { erreur = ex.Message });
            }
        })
        .RequireAuthorization(policy => policy.RequireRole("Planneur", "Admin"));

        return app;
    }
}
