using Domain.Entities;
using MediatR;

namespace App.Users.Commands;

public class CreateParticipant : IRequest<Participants>
{
    public string? UserId { get; set; }
    public string? Name { get; set; }
}