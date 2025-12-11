using Domain.Entities;

namespace Domain.Interface;

public interface IChatMessageRepository
{
    Task SaveMessageAsync(Messages message, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Messages>> GetMessagesByChatId(Guid chatId, int limit, CancellationToken ct);
}