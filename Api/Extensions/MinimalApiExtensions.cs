using App.Abstractions;
using App.Chat.Commands;
using Infra;
using Infra.Repository;

namespace Api.Extensions;

public static class MinimalApiExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<InfraDbContext>();
        serviceCollection.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChatCommand).Assembly));

        serviceCollection.AddScoped<IChatRepository, ChatRepository>();

        return serviceCollection;
    }
}