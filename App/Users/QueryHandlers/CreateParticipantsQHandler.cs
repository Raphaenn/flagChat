using App.Abstractions;
using App.Users.Queries;
using Domain.Entities;
using MediatR;

namespace App.Users.QueryHandlers;

public class CreateParticipantsQHandler : IRequestHandler<CreateParticipantQuery, Participants>
{
    private readonly IParticipantsRepository _participantsRepository;

    public CreateParticipantsQHandler(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task<Participants> Handle(CreateParticipantQuery request, CancellationToken cancellationToken)
    {
        Participants data = new Participants(userId: request.UserId, name: request.Name);
        
        return await _participantsRepository.CreateParticipant(data);
    }
}