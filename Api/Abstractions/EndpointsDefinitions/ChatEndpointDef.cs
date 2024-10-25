using App.Chat.Commands;
using Domain.Entities;
using Infra.SignalRContext;
using MediatR;

namespace Api.Abstractions.EndpointsDefinitions;

public class ChatEndpointDef : IEndpointsDefinitions
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapHub<SignalRConnectionHub>("/chatHub").RequireAuthorization();

        app.MapPost("/chat/create", async (HttpContext context, IMediator mediator, string user1, string user2) =>
        {
            List<string> parts = new List<string>
            {
                user1,
                user2
            };

            CreateChatCommand newChatCommand = new CreateChatCommand
            {
                Users = parts,
                CreatedAt = DateTime.Now,
            };
            
            var post = await mediator.Send(newChatCommand);
            return Results.Ok(post);
        });

        // app.MapPost("/send", async (string text) =>
        // {
        //     var x = new SignalRConnectionHub();
        //     x.SendMessage("Raphael", text);
        //     Results.Ok();
        // });
    }
}