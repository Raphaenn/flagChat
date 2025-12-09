using Domain.Entities;
using Domain.Interface;

namespace Infra.Repository;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly InfraDbContext _infraDbContext;

    public ChatMessageRepository(InfraDbContext infraDbContext)
    {
        this._infraDbContext = infraDbContext;
    }

    public async Task SaveMessageAsync(Messages message, CancellationToken ct)
    {
        _infraDbContext.Message?.Add(message);
        await _infraDbContext.SaveChangesAsync(ct);
    }

    public async Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}