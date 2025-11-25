using Domain.Entities;
using MediatR;

namespace App.Users.Queries;

public class CreateParticipantQuery : IRequest<Participants>
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
}