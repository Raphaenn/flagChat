using System.Security.Claims;
using App.Chat.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class ChatHub : Hub
{
    // private readonly IChatService _chatService;
    //
    // public ChatHub(IChatService chatService)
    // {
    //     _chatService = chatService;
    // }
    
    // CreateChatCommand newChatCommand = new CreateChatCommand
    // {
    //     ParticipantId1 = Guid.NewGuid(),
    //     ParticipantId2 = Guid.NewGuid(),
    //     Status = "active",
    //     CreatedAt = DateTime.Now,
    // };
    //         
    // var post = await mediator.Send(newChatCommand);
    //     return Results.Ok(post);
    
    private Guid GetCurrentUserId()
    {
        var sub = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(sub);
        if (string.IsNullOrWhiteSpace(sub))
            throw new HubException("User not found.");

        return Guid.Parse(sub);
    }
    
    public async override Task OnConnectedAsync()
    {
        // Aqui você pode logar, registrar connectionId em algum lugar, etc.
        await base.OnConnectedAsync();
    }
    
    /// <summary>
    /// Init a chat with destiny user:
    /// - obtém ou cria o chat,
    /// - carrega últimas mensagens,
    /// - adiciona o caller ao grupo do chat,
    /// - devolve o histórico.
    /// </summary>
    public async Task StartChat(string targetUserId, IMediator mediator)
    {
        var currentUserId = GetCurrentUserId();
        var targetIdGuid = Guid.Parse(targetUserId);

        // 1. Serviço cuida de verificar couple + criar/pegar chat + histórico
        var startChat = new StartChatCommand();
        var chat = await mediator.Send(startChat);
        // var chatDto = await _chatService.StartChatAsync(currentUserId, targetIdGuid);

        // 2. Adiciona o caller ao grupo desse chat
        await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());

        // 3. Devolve o histórico para o caller
        // await Clients.Caller.SendAsync("ChatLoaded", chat.Id, chat.LastMessages);
    }

    /// <summary>
    /// Envia mensagem dentro de um chat existente.
    /// </summary>
    public async Task SendMessage(string chatId, string text, IMediator mediator)
    {
        var currentUserId = GetCurrentUserId();
        var chatGuid = Guid.Parse(chatId);

        // 1. Salva e valida pelo serviço
        // var messageDto = await _chatService.SendMessageAsync(chatGuid, currentUserId, text);

        // 2. Broadcast da mensagem para todo mundo no grupo (no máximo 2 pessoas)
        // await Clients.Group(chatId).SendAsync("ReceiveMessage", messageDto);
    }

}