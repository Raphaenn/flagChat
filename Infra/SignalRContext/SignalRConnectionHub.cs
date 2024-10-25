using Microsoft.AspNetCore.SignalR;

namespace Infra.SignalRContext;
public class SignalRConnectionHub : Hub
{
    private readonly InfraDbContext _infraDbContext;
    
    public SignalRConnectionHub(InfraDbContext infraDbContext)
    {
        // Console.WriteLine(Context.ConnectionId);
        _infraDbContext = infraDbContext;

    }
    
    // levantar conn quando identificar dois usuários que sao amigos
    public async Task SendMessage(string userName, string message)
    {
        // identificar usuário logado
        
        // identificar usuário destino
        
        // verificar se já existe um chat entre eles
        
        // caso exista carrega o chat existente
        
        // caso não exista, crei um novo
        
        // envia a mensage diretamente
        
        // salva todas mensagens de forma async no banco
        

        var claimsPrincipal = Context.User;
        foreach (var claim in claimsPrincipal.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }
        
        // Antes de mandar a mensagem eu quero verificar se o usuário ativo tem um registro couple com o usuário destino.
        // Caso não tenha eu retorno um erro e caso tenha eu disparo a mensagem.
        
        await Clients.Client(userName).SendAsync("ReceiveMessage", message);

    }
}