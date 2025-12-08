using Domain.Entities;

namespace Domain.Interface;

public interface IParticipantRepository
{
    Task<Participants> CreateParticipant(Participants participant, CancellationToken ct);
    
    Task<List<Participants>> GetParticipants(Guid userId, CancellationToken ct);
    
    Task<bool> ExistsParticipantsAsync(Guid userId, CancellationToken ct);
}