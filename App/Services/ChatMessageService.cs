using App.Interfaces.IQueueService;
using Domain.Entities;
using Domain.Interface;

namespace App.Services;

public class ChatMessageService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IChatMessageRepository _repository;

    public ChatMessageService(IBackgroundTaskQueue taskQueue, IChatMessageRepository repository)
    {
        _taskQueue = taskQueue;
        _repository = repository;
    }

    public void ProcessCustomerAsync(Guid chatId, Guid senderId, string content)
    {
        _taskQueue.QueueBackgroundWorkItemAsync(async token =>
        {
            // This is executed in the background
            Messages msg = Messages.CreateMessage(chatId, senderId, content, DateTime.Now);
            await _repository.SaveMessageAsync(msg);
        });
    }
}