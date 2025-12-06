using System.Security.Claims;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.SignalR;


namespace Infra.SignalRContext;
public class SignalRConnectionHub : Hub
{
    private readonly IParticipantRepository _participantsRepository;
    private readonly IConnectionManager _connectionManager;
    
    public SignalRConnectionHub(IConnectionManager connectionManager, IParticipantRepository participantsRepository)
    {
        _connectionManager = connectionManager;
        _participantsRepository = participantsRepository;
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(userId);

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

    // levantar conn quando identificar dois usuários que sao amigos
    public async Task SendMessageToUser(string userId, string message)
    {
        // Guid parsedUserId = Guid.Parse(userId);
        
        // todo - Verifica se o usuário de destino existe no banco de dados
        // List<Participants> targetUser = await _participantsRepository.GetParticipants(parsedUserId);
        // if (targetUser == null)
        //     throw new HubException("User not found");

        // Recupera todas as conexões do usuário de destino
        var connections = _connectionManager.GetConnections(userId);
        foreach (var connectionId in connections)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
        
        // identificar usuário destino
        
        // verificar se já existe um chat entre eles
        
        // caso exista carrega o chat existente
        
        // caso não exista, crei um novo
        
        // envia a mensage diretamente
        
        // salva todas mensagens de forma async no banco

        // Antes de mandar a mensagem eu quero verificar se o usuário ativo tem um registro couple com o usuário destino.
        
        // Caso não tenha eu retorno um erro e caso tenha eu disparo a mensagem.
        
        // await Clients.Client(userId).SendAsync("ReceiveMessage", message);

    }
}