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

    public void SaveMessage(Messages message)
    {
        _infraDbContext.Message?.Add(message);
        _infraDbContext.SaveChanges();
    }

    public async Task<List<Messages>> GetLastMessagesAsync(Guid chatId, int limit, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}