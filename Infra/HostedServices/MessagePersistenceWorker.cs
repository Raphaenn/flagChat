using App.Chat.Dto;
using App.Interfaces.IQueueService;
using Domain.Interface;
using Infra.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.HostedServices;

public class MessagePersistenceWorker : BackgroundService
{

    private readonly MessageBackgroundTaskQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MessagePersistenceWorker> _logger;

    public MessagePersistenceWorker(
        MessageBackgroundTaskQueue queue,
        IServiceScopeFactory scopeFactory,
        ILogger<MessagePersistenceWorker> logger)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("MessagePersistenceWorker started.");

        await foreach (PersistChatMessageJob job in _queue.DequeueAllAsync(stoppingToken))
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var chatMessageRepository = scope.ServiceProvider
                    .GetRequiredService<IChatMessageRepository>();

                // Aqui você monta o objeto de domínio Messages
                var messageEntity = Domain.Entities.Messages.CreateMessage(
                    job.ChatId,
                    job.SenderId,
                    job.Content,
                    job.CreatedAtUtc
                );

                await chatMessageRepository.SaveMessageAsync(messageEntity, stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError(ex,
                    "Erro ao processar PersistChatMessageJob para ChatId {ChatId}, SenderId {SenderId}",
                    job.ChatId, job.SenderId);
            }
        }

        _logger.LogInformation("MessagePersistenceWorker stopping.");
    }
}