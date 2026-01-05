using System.Security.Claims;
using App.Interfaces.Chat;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class SignalRConnectionHub : Hub
{
    private readonly IConnectionManager _connectionManager;
    private readonly ISendMessageService _sendMessageService;
    
    public SignalRConnectionHub(
        IConnectionManager connectionManager, 
        ISendMessageService sendMessageService
        )
    {
        _connectionManager = connectionManager;
        _sendMessageService = sendMessageService;
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

    public async Task SendMessageToUser(string targetId, string message)
    {
        var cancellationToken = Context.ConnectionAborted; // catch cancellationToken
        string? senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(senderId);
        
        if (!Guid.TryParse(senderId, out var senderGuid))
            throw new HubException("Invalid sender id");
        
        if (!Guid.TryParse(targetId, out var targetGuid))
            throw new HubException("Invalid target id");
        
        // 1) Call sendMessage service
        await _sendMessageService.SendMessageAsync(senderGuid, targetGuid, message, cancellationToken);
        
        // 2) Recovery all destination user connections (An user can be multiples connections)
        var connections = _connectionManager.GetConnections(targetId);
        foreach (var connectionId in connections)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message, cancellationToken: cancellationToken);
        }
        
    }
}