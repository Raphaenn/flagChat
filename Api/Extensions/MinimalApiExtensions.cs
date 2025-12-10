using App.Chat.Commands;
using App.Interfaces.Chat;
using App.Interfaces.IQueueService;
using App.Services;
using Domain.Interface;
using Infra;
using Infra.HostedServices;
using Infra.Repository;
using Infra.Services;
using Infra.SignalRContext;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class MinimalApiExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        serviceCollection.AddSingleton<MongoDbContext>();
        
        // Fila
        serviceCollection.AddSingleton<MessageBackgroundTaskQueue>(); // concreto
        serviceCollection.AddSingleton<IMessagePersistenceQueue>(sp =>
            sp.GetRequiredService<MessageBackgroundTaskQueue>()); // interface de App apontando pro mesmo singleton

        // Worker
        serviceCollection.AddHostedService<MessagePersistenceWorker>();
        
        // AppService
        serviceCollection.AddScoped<ISendMessageService, SendMessageAppService>();

        serviceCollection.AddDbContext<InfraDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        // serviceCollection.AddSignalR();
        serviceCollection.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
        });
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChatCommand).Assembly));

        serviceCollection.AddSingleton<IConnectionManager, SignalConnections>();
        serviceCollection.AddScoped<IParticipantRepository, ParticipantsRepository>();
        serviceCollection.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        serviceCollection.AddScoped<IChatRepository, ChatRepository>();

        return serviceCollection;
    }
}