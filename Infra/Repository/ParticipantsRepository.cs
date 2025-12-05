using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class ParticipantsRepository : IParticipantRepository
{
    private readonly InfraDbContext _infraDbContext;

    public ParticipantsRepository(InfraDbContext infraDbContext)
    {
        this._infraDbContext = infraDbContext;
    }

    public async Task<ICollection<Participants>> GetParticipants()
    {
        return await _infraDbContext.Participants?.ToListAsync()!;
    }

    public async Task<Participants?> GetParticipant(string email)
    {
        if (_infraDbContext.Participants != null)
        {
            return await _infraDbContext.Participants.FirstOrDefaultAsync(p => p.Email == email);
        }

        return null;
    }

    public async Task<Participants> DeleteParticipant()
    {
        throw new NotImplementedException();
    }

    public async Task<Participants> CreateParticipant(Participants participant, CancellationToken ct)
    {
        _infraDbContext.Participants?.Add(participant);
        await _infraDbContext.SaveChangesAsync(ct);
        return participant;
    }

    public async Task<List<Participants>> GetParticipants(Guid userId)
    {
        return await _infraDbContext.Participants?.ToListAsync()!;
    }

    public async Task<bool> ExistsParticipantsAsync(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}