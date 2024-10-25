using System.Security.Claims;
using App.Users.Commands;
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

        app.MapPost("/create", async (HttpContext httpContext, IMediator mediator, string name) =>
        {
            Console.WriteLine("opa");
            var user = httpContext.User;
            
            // foreach (var claim in user.Claims)
            // {
            //     Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            // }
            
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Verificar se o usuário está autenticado
            // if (!user.Identity.IsAuthenticated)
            // {
            //     return Results.Unauthorized();
            // }
            
            var create = new CreateParticipant
            {
                UserId = userId,
                Name = name
            };
            var post = await mediator.Send(create);
            return Results.Ok(post);
        }).RequireAuthorization();
    }
}