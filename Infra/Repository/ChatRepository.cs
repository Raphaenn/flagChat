using Domain.Interface;
using Domain.Entities;
using MongoDB.Driver;

namespace Infra.Repository;

public class ChatRepository : IChatRepository
{
    private readonly InfraDbContext _infraDbContext;
    
    public ChatRepository(InfraDbContext infraDbContext)
    {
        _infraDbContext = infraDbContext;
    }
    
    public async Task StartChat(Chats chat, CancellationToken ct)
    {
        _infraDbContext.Chats?.Add(chat);
        await _infraDbContext.SaveChangesAsync(ct);
    }

    public async Task<Chats> GetChatById()
    {
        throw new NotImplementedException();
    }

    public async Task<Chats?> SearchChatByParticipants(Guid userId1, Guid userId2, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Chats?> GetByIdAsync(Guid chatId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}