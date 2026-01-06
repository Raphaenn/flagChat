using System.Security.Claims;
using Api.Hubs;
using App.Chat.Commands;
using App.Chat.Queries;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ChatEndpointDef : IEndpointsDefinitions
{
    private record struct ChatListReq(string UserId);
    private record struct CreateChatReq(string User1, string User2);
    
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/health-check",() => Results.Ok());
        
        app.MapHub<SignalRConnectionHub>("/chathub").RequireAuthorization();

        // todo - create chat endpoint (ensure security) 
        app.MapPost("/chat/create", async (HttpContext context, IMediator mediator) =>
        {
            var data = await context.Request.ReadFromJsonAsync<CreateChatReq>();
            
            CreateChatCommand newChatCommand = new CreateChatCommand
            {
                ParticipantId1 = Guid.Parse(data.User1),
                ParticipantId2 = Guid.Parse(data.User2),
                Status = "active",
                CreatedAt = DateTime.Now,
            };
            
            var post = await mediator.Send(newChatCommand);
            return Results.Ok(post);
        }).RequireAuthorization();
        
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

        app.MapPost("/chat/message/recovery", async (HttpContext context) =>
        {
            return Results.Ok();
        });
    }
}