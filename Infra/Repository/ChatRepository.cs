using App.Abstractions;
using Domain.Entities;
using MongoDB.Driver;

namespace Infra.Repository;

public class ChatRepository : IChatRepository
{
    private readonly IMongoCollection<Chat> _chatCollection;
    
    public ChatRepository(MongoDbContext chatCollection)
    {
        this._chatCollection = chatCollection.GetCollection<Chat>("chats");
    }
    
    public async Task CreateChat(Chat data)
    {
        await _chatCollection.InsertOneAsync(data);
    }

    public async Task<Chat> GetChatById()
    {
        throw new NotImplementedException();
    }

    public async Task<Chat> GetChatUserId(string userId)
    {
        // Filtro para verificar se o campo Participants contém o ID
        var filter = Builders<Chat>.Filter.AnyEq(field: "Participants", userId);
        
        // Busca o primeiro chat que contém o usuário
        Chat response = await _chatCollection.Find(filter).FirstOrDefaultAsync();
        return response;
    }

    public async Task DeleteChat()
    {
        throw new NotImplementedException();
    }
}