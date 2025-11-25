using Domain.Entities;
using MediatR;

namespace App.Participant.Commands;

public class CreateParticipant : IRequest<Participants>
{
    public string? Email { get; set; }
}