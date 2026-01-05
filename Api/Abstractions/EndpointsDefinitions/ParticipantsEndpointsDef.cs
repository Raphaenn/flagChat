using System.Security.Claims;
using App.Participant.Commands;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ParticipantsEndpointsDef : IEndpointsDefinitions
{
    private record struct CreateReq(string Email);

    public void RegisterEndpoints(WebApplication app)
    {
         
        var group = app.MapGroup("/api/participants");
        
        group.MapGet("/chat", async () =>
        {
            Results.Ok();
        });

        group.MapPost("/create", async (HttpContext context, IMediator mediator) =>
        {
            // Pegar contexto do user logado
            var user = context.User;
            var request = await context.Request.ReadFromJsonAsync<CreateReq>();
            
            // Pega a claim de name NameIdentifier
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            // Verificar se o usuário está autenticado
            if (!user.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }
            
            var create = new CreateParticipant
            {
                UserId = Guid.Parse(userId),
                Email = request.Email
            };
            var post = await mediator.Send(create);
            return Results.Ok(post);
        }).RequireAuthorization();
    }
}