using Domain.Interface;
using Domain.Entities;
using MongoDB.Driver;

namespace Infra.Repository;

public class ChatRepository : IChatRepository
{
    private readonly IMongoCollection<Chats> _chatCollection;
    
    public ChatRepository(MongoDbContext chatCollection)
    {
        this._chatCollection = chatCollection.GetCollection<Chats>("chats");
    }
    
    public async Task CreateChat(Chats data)
    {
        await _chatCollection.InsertOneAsync(data);
    }

    public async Task<Chats> GetChatById()
    {
        throw new NotImplementedException();
    }

    public async Task<Chats> GetChatUserId(string userId)
    {
        // Filtro para verificar se o campo Participants contém o ID
        var filter = Builders<Chats>.Filter.AnyEq(field: "Participants", userId);
        
        // Busca o primeiro chat que contém o usuário
        Chats response = await _chatCollection.Find(filter).FirstOrDefaultAsync();
        return response;
    }

    public async Task DeleteChat()
    {
        throw new NotImplementedException();
    }

    public async Task<Chats> StartChat(Chats chat, CancellationToken ct)
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