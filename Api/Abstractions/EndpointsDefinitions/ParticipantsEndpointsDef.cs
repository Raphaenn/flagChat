using System.Security.Claims;
using App.Participant.Commands;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ParticipantsEndpointsDef : IEndpointsDefinitions
{

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGroup("/api/participants/");
        
        app.MapGet("/chat", async () =>
        {
            Results.Ok();
        });

        app.MapPost("/create", async (HttpContext httpContext, IMediator mediator, string email) =>
        {
            // Pegar contexto do user logado
            var user = httpContext.User;
            
            // lista as claims desse user
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            
            // Pega a claim de name NameIdentifier
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            // Verificar se o usuário está autenticado
            if (!user.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            
            var create = new CreateParticipant
            {
                Email = email
            };
            var post = await mediator.Send(create);
            return Results.Ok(post);
        }).RequireAuthorization();
    }
}