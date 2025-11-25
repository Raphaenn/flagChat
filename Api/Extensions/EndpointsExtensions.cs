using Api.Abstractions;

namespace Api.Extensions;

public static class EndpointsExtensions
{
    public static void RegisterEndpointsDefinitions(this WebApplication application)
    {
        IEnumerable<IEndpointsDefinitions> endpointsdefinitions = typeof(Program).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointsDefinitions)) && !t.IsAbstract && !t.IsInterface).Select(Activator.CreateInstance).Cast<IEndpointsDefinitions>();

        foreach (var endpoints in endpointsdefinitions)
        {
            endpoints.RegisterEndpoints(application);
        }
    }
}

// Objetivo do Método RegisterEndpointsDefinitions:
// O método RegisterEndpointsDefinitions percorre todas as classes que implementam a interface IEndpointsDefinitions, criando instâncias dessas classes e chamando o método RegisterEndpoints para registrar os endpoints de cada uma.

// Localização das Implementações de IEndpointsDefinitions:
// typeof(Program).Assembly.GetTypes() obtém todos os tipos (classes, interfaces, etc.) no assembly onde a classe Program está localizada (normalmente, o assembly principal da aplicação).

// .Where(t => t.IsAssignableTo(typeof(IEndpointsDefinitions)) && !t.IsAbstract && !t.IsInterface) filtra apenas os tipos que:
// Implementam a interface IEndpointsDefinitions
// Não são classes abstratas (!t.IsAbstract)
// Não são interfaces (!t.IsInterface)

// Instanciação e Registro dos Endpoints:
// .Select(Activator.CreateInstance).Cast<IEndpointsDefinitions>() cria uma instância de cada classe que implementa IEndpointsDefinitions.
// Um foreach percorre cada instância (endpoints) e chama o método RegisterEndpoints(application), que registra os endpoints na instância de WebApplication passada.