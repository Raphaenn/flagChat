using System.Threading.Channels;
using App.Chat.Dto;
using App.Interfaces.IQueueService;

namespace Infra.Services;

public class MessageBackgroundTaskQueue : IMessagePersistenceQueue
{
    private readonly Channel<PersistChatMessageJob> _channel;

    public MessageBackgroundTaskQueue()
    {
        // Pode usar bounded se quiser limitar capacidade
        _channel = Channel.CreateUnbounded<PersistChatMessageJob>();
    }

    public async Task EnqueueAsync(PersistChatMessageJob job, CancellationToken ct)
    {
        await _channel.Writer.WriteAsync(job, ct);
    }

    // Exposto apenas para o worker ler
    public IAsyncEnumerable<PersistChatMessageJob> DequeueAllAsync(CancellationToken ct)
        => _channel.Reader.ReadAllAsync(ct);
}
