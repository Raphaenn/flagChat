using Domain.Entities;
using MediatR;

namespace App.Chat.Commands;

public class CreateChatCommand : IRequest<Chats>
{
    public Guid ParticipantId1 { get; set; }
    public Guid ParticipantId2 { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}