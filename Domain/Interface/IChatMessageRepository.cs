using Domain.Entities;

namespace Domain.Interface;

public interface IChatMessageRepository
{
    Task<Messages> AddAsync(Messages message, CancellationToken ct = default);
    Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default);
}