using Domain.Entities;
using MediatR;

namespace App.Chat.Commands;

public class StartChatCommand : IRequest<Chats>
{
    public Guid CurrentUserId { get; set; } 
    public Guid TargetUserId { get; set; } 
    public DateTime UpdatedAt { get; set; }
}