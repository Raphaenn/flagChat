using App.Abstractions;
using App.Chat.Commands;
using Domain.Entities;
using MediatR;

namespace App.Chat.CommandHandlers;

public class CreateChatHandlers : IRequestHandler<CreateChatCommand, Chats>
{
    private readonly IChatRepository _chatRepository;

    public CreateChatHandlers(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chats> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        Chats chat = Chats.CreateChat(request.ParticipantId1, request.ParticipantId2, request.Status, request.CreatedAt);
        await _chatRepository.CreateChat(chat);
        return chat;
    }
}