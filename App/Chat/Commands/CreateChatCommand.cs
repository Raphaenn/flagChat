using Domain.Entities;
using MediatR;

namespace App.Chat.Commands;

public class CreateChatCommand : IRequest<Chats>
{
    public List<string> Users { get; set; }
    public DateTime CreatedAt { get; set; }
}