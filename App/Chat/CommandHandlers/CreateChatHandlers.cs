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
        Chats chat = new Chats(participants: request.Users, DateTime.Now);
        await _chatRepository.CreateChat(chat);
        return chat;
    }
}