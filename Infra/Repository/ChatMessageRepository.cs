using Domain.Entities;
using Domain.Interface;

namespace Infra.Repository;

public class ChatMessageRepository : IChatMessageRepository
{

    public async Task<Messages> AddAsync(Messages message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}