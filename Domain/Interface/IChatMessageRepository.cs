using Domain.Entities;

namespace Domain.Interface;

public interface IChatMessageRepository
{
    void SaveMessage(Messages message);
    Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default);
}