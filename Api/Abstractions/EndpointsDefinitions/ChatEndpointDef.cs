using System.Security.Claims;
using App.Chat.Commands;
using Domain.Entities;
using Infra.SignalRContext;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ChatEndpointDef : IEndpointsDefinitions
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapHub<SignalRConnectionHub>("/chathub").RequireAuthorization();

        app.MapPost("/chat/create", async (HttpContext context, IMediator mediator, string user1, string user2) =>
        {
            CreateChatCommand newChatCommand = new CreateChatCommand
            {
                ParticipantId1 = Guid.NewGuid(),
                ParticipantId2 = Guid.NewGuid(),
                Status = "active",
                CreatedAt = DateTime.Now,
            };
            
            var post = await mediator.Send(newChatCommand);
            return Results.Ok(post);
        }).RequireAuthorization();

        app.MapGet("/health-check",() => Results.Ok());  
    }
}