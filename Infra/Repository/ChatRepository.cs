using App.Abstractions;
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

    public async Task<Chats> GetChatUserId()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteChat()
    {
        throw new NotImplementedException();
    }
}