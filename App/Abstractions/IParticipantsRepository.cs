using Domain.Entities;

namespace App.Abstractions;
public interface IParticipantsRepository
{
    Task<ICollection<Participants>> GetParticipants();
    
    Task<Participants?> GetParticipant(string userId);

    Task<Participants> CreateParticipant(Participants data);

    Task<Participants> DeleteParticipant();
}