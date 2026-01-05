using App.Chat.Commands;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Chat.CommandHandlers;

public class CreateChatCmdHandler : IRequestHandler<CreateChatCommand, Chats>
{
    private readonly IChatRepository _chatRepository;

    public CreateChatCmdHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task<Chats> Handle(CreateChatCommand request, CancellationToken ct)
    {
        Chats newChat = Chats.StartChat(request.ParticipantId1, request.ParticipantId2, request.Status, request.CreatedAt);

        await _chatRepository.StartChat(newChat, ct);
        return newChat;
    }
}