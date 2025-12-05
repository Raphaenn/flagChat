using App.Chat.Commands;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Chat.CommandHandlers;

public class StartChatCmdHandlers : IRequestHandler<StartChatCommand, Chats>
{
    private readonly IChatRepository _chatRepository;

    public StartChatCmdHandlers(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chats> Handle(StartChatCommand request, CancellationToken cancellationToken)
    {
        // before starts a chat we need to check if alread exists one. If not, start a new chat with status active. If a chat already exists check the status and return a connection with status != blocked
        
        Chats? search = await _chatRepository.SearchChatByParticipants(request.CurrentUserId, request.TargetUserId, cancellationToken);

        if (search == null )
        {
            Chats newChat = Chats.StartChat(
                request.CurrentUserId, 
                request.TargetUserId, 
                "active",
                request.UpdatedAt);

            return newChat;
        }

        if (search.Status == "blocked")
            throw new ApplicationException("Invalid chat status");
        
        Chats chat = Chats.StartChat(
            request.CurrentUserId, 
            request.TargetUserId, 
            "active", 
            request.UpdatedAt);
        
        await _chatRepository.StartChat(chat, cancellationToken);
        
        return chat;
    }
}