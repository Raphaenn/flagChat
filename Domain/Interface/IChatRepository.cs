using Domain.Entities;

namespace Domain.Interface;

public interface IChatRepository
{
    Task StartChat(Chats chat, CancellationToken ct);
    
    Task<Chats?> SearchChatByParticipants(Guid userId1, Guid userId2, CancellationToken ct);
   
    Task<Chats?> GetByIdAsync(Guid chatId, CancellationToken ct = default);

    Task<IReadOnlyList<Chats>?> GetAllChatsByParticipantId(Guid pId, CancellationToken ct);

}