using App.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class ParticipantsRepository : IParticipantsRepository
{
    private readonly InfraDbContext _infraDbContext;

    public ParticipantsRepository(InfraDbContext infraDbContext)
    {
        this._infraDbContext = infraDbContext;
    }

    public async Task<ICollection<Participants>> GetParticipants()
    {
        return await _infraDbContext.Participants.ToListAsync();
    }

    public async Task<Participants> CreateParticipant(Participants data)
    {
        _infraDbContext.Participants.Add(data);
        await _infraDbContext.SaveChangesAsync();
        return data;
    }

    public async Task<Participants> DeleteParticipant()
    {
        throw new NotImplementedException();
    }
}