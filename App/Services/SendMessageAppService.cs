using App.Chat.Dto;
using App.Interfaces.Chat;
using App.Interfaces.IQueueService;
using Domain.Entities;
using Domain.Interface;

namespace App.Services;

public class SendMessageAppService : ISendMessageService
{
    private readonly IParticipantRepository _participantsRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IMessagePersistenceQueue _messagePersistenceQueue;


    public SendMessageAppService(
        IParticipantRepository participantRepository,
        IChatRepository chatRepository,
        IMessagePersistenceQueue messagePersistenceQueue
        )
    {
        _participantsRepository = participantRepository;
        _chatRepository = chatRepository;
        _messagePersistenceQueue = messagePersistenceQueue;
    }


    public async Task SendMessageAsync(Guid senderId, Guid targetId, string message, CancellationToken ct)
    {
        Participants? sender = await _participantsRepository.GetParticipants(senderId, ct); 
        Participants? target = await _participantsRepository.GetParticipants(targetId, ct);

        if (target == null || sender == null)
            throw new ApplicationException("Wrong participants");
        
        // 1) Check is an participants chat exists (Created when the users became friends.)
        Chats? chat = await _chatRepository.SearchChatByParticipants(sender.Id, target.Id, ct);
        if(chat == null)
            throw new ApplicationException("Invalid chat participants");
        
        // Save async message on db
        // 3) Enfileira job de persistência da mensagem
        var job = new PersistChatMessageJob(
            ChatId: chat.Id,
            SenderId: sender.Id,
            Content: message,
            CreatedAtUtc: DateTime.UtcNow
        );

        await _messagePersistenceQueue.EnqueueAsync(job, ct);
    }
}

/*4. Detalhes menores
a) Tipo de exceção na Application
Você está usando ApplicationException. Hoje em dia ela é meio desaconselhada.
Melhor:
Criar suas próprias, ex: ParticipantsNotFoundException, ChatNotFoundException
Ou retornar algum tipo de Result (ex: OneOf, ErrorOr, etc.) se quiser ficar mais funcional.*/