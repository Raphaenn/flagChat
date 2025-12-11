using App.Chat.Dto;
using MediatR;

namespace App.Chat.Queries;

public class SearchChatByUserIdQuery : IRequest<ChatMessageDto>
{
    public Guid UserId { get; set; }
}