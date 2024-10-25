using App.Abstractions;
using App.Users.Commands;
using Domain.Entities;
using MediatR;

namespace App.Users.CommandsHandlers;

public class CreateParticipantHandler : IRequestHandler<CreateParticipant, Participants>
{
    private readonly IParticipantsRepository _participantsRepository;

    public CreateParticipantHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task<Participants> Handle(CreateParticipant request, CancellationToken cancellationToken)
    {
        Participants data = new Participants(userId: request.UserId, name: request.Name);
        return await _participantsRepository.CreateParticipant(data);
    }
}