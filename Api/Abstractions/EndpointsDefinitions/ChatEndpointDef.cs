using System.Security.Claims;
using Api.Hubs;
using App.Chat.Commands;
using App.Chat.Queries;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ChatEndpointDef : IEndpointsDefinitions
{
    private record struct ChatListReq(string UserId); 
    
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/health-check",() => Results.Ok());
        
        app.MapHub<SignalRConnectionHub>("/chathub").RequireAuthorization();

        // todo - create chat endpoint (ensure security) 
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
        
        // search chats by user id
        app.MapPost("/chats/list", async (HttpContext context, IMediator mediator) =>
        {
            var request = await context.Request.ReadFromJsonAsync<ChatListReq>();
            SearchChatByUserIdQuery query = new SearchChatByUserIdQuery
            {
                UserId = Guid.Parse(request.UserId)
            };

            var chats = await mediator.Send(query);
            return Results.Ok(chats);
        });
    }
}