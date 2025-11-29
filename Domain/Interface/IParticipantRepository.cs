namespace Domain.Interface;

public interface IParticipantRepository
{
    Task<bool> ExistsParticipantsAsync(Guid userId, CancellationToken ct);
}