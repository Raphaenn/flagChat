using MediatR;
using Domain.Entities;

namespace App.Message.Commands;

public class SendMessageCommand : IRequest<Messages>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}