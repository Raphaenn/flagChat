using App.Abstractions;
using App.Participant.Commands;
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
        Participants data = Participants.CreateParticipant(request.Email);
        return await _participantsRepository.CreateParticipant(data);
    }
}