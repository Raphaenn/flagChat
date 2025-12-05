using App.Users.Queries;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Users.QueryHandlers;

public class CreateParticipantsQHandler : IRequestHandler<CreateParticipantQuery, Participants>
{
    private readonly IParticipantRepository _participantsRepository;

    public CreateParticipantsQHandler(IParticipantRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task<Participants> Handle(CreateParticipantQuery request, CancellationToken cancellationToken)
    {
        Participants data = Participants.CreateParticipant(request.UserId, request.Email);
        
        return await _participantsRepository.CreateParticipant(data, cancellationToken);
    }
}