using Domain.Entities;

namespace Domain.Interface;

public interface IChatMessageRepository
{
    Task SaveMessageAsync(Messages message, CancellationToken cancellationToken = default);
    Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default);
}