using App.Chat.Commands;
using Domain.Interface;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class MinimalApiExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        serviceCollection.AddSingleton<MongoDbContext>();

        serviceCollection.AddDbContext<InfraDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        serviceCollection.AddSignalR();
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChatCommand).Assembly));

        // serviceCollection.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        serviceCollection.AddScoped<IChatRepository, ChatRepository>();

        return serviceCollection;
    }
}