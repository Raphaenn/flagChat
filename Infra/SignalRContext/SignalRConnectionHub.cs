using System.Security.Claims;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infra.SignalRContext;

[Authorize]
public class SignalRConnectionHub : Hub
{
    private readonly IParticipantRepository _participantsRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IConnectionManager _connectionManager;
    
    public SignalRConnectionHub(
        IConnectionManager connectionManager, 
        IParticipantRepository participantsRepository, 
        IChatRepository chatRepository, 
        IChatMessageRepository chatMessageRepository
        )
    {
        _connectionManager = connectionManager;
        _participantsRepository = participantsRepository;
        _chatRepository = chatRepository;
        _chatMessageRepository = chatMessageRepository;
    }

    public override Task OnConnectedAsync()
    {   
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
            _connectionManager.AddConnection(userId, Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
          _connectionManager.RemoveConnection(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
    
    public void RegisterUser(string userId)
    {
        _connectionManager.AddConnection(userId, Context.ConnectionId);
        Console.WriteLine($"User {userId} registered with connection {Context.ConnectionId}");
    }

    public async Task SendMessageToUser(string targetId, string message, CancellationToken cancellationToken)
    {
        string? senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(senderId))
            throw new HubException("Unauthorized");

        if (!Guid.TryParse(senderId, out var senderGuid))
            throw new HubException("Invalid sender id");

        if (!Guid.TryParse(targetId, out var targetGuid))
            throw new HubException("Invalid target id");
        
        // 1) Check is an participants chat exists (Created when the users became friends.)
        Chats? chat = await _chatRepository.SearchChatByParticipants(senderGuid, targetGuid, cancellationToken);
        if(chat == null)
            throw new HubException("Invalid chat participants");
        
        // 2) check if destination user exists
        var targetUser = await _participantsRepository.GetParticipants(targetGuid, cancellationToken);
        if (targetUser == null)
            throw new HubException("User not found");

        // 3) Recovery all destination user connections (An user can be multiples connections)
        var connections = _connectionManager.GetConnections(targetId);

        // Save async message on db
        Messages msg = Messages.CreateMessage(chat.Id, senderGuid, message, DateTime.Now);
        _chatMessageRepository.SaveMessage(msg);
        
        foreach (var connectionId in connections)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message, cancellationToken: cancellationToken);
        }
    }
}