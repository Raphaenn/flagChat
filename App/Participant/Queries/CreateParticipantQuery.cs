using Domain.Entities;
using MediatR;

namespace App.Users.Queries;

public class CreateParticipantQuery : IRequest<Participants>
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
}