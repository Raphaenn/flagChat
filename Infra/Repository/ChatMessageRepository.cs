using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<Messages>> GetMessagesByChatId(Guid chatId, int limit, CancellationToken ct = default)
    {
        var msg = await _infraDbContext.Message.Where(m => m.ChatId == chatId).Take(20).ToListAsync(ct);
        return msg;
    }
}