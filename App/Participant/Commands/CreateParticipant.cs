using Domain.Entities;
using MediatR;

namespace App.Participant.Commands;

public class CreateParticipant : IRequest<Participants>
{
    public Guid UserId { get; set; }
    public string Email { get; init; } = default!;
}