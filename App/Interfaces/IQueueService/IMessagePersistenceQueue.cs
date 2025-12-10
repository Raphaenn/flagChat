using App.Chat.Dto;

namespace App.Interfaces.IQueueService;

public interface IMessagePersistenceQueue
{
    Task EnqueueAsync(PersistChatMessageJob job, CancellationToken ct);
}