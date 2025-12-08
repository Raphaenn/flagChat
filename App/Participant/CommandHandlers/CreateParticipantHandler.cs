using App.Participant.Commands;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Participant.CommandsHandlers;

public class CreateParticipantHandler : IRequestHandler<CreateParticipant, Participants>
{
    private readonly IParticipantRepository _participantsRepository;

    public CreateParticipantHandler(IParticipantRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task<Participants> Handle(CreateParticipant request, CancellationToken cancellationToken)
    {
        Participants data = Participants.CreateParticipant(request.UserId, request.Email);
        return await _participantsRepository.CreateParticipant(data, cancellationToken);
    }
}