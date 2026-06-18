using Api.Models;
using Api.Services;
using Core.UseCases.Abstractions;

namespace Api.EndPoints;

public static class AuthRoutes
{
    public static WebApplication AddAuthRoutes(this WebApplication app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapPost("login", (LoginRequest request, IUserUseCases userUseCases, TokenService tokenService) =>
        {
            var utilisateur = userUseCases.Login(request.Email, request.MotDePasse);
            if (utilisateur is null)
                return Results.Unauthorized();

            var token = tokenService.GenerateToken(utilisateur);
            return Results.Ok(new { token });
        });

        return app;
    }
}
