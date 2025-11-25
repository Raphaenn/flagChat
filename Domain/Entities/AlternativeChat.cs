using System.Collections.ObjectModel;

namespace Domain.Entities;

public class AlternativeChat
{
    public Guid Id { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Only chat can add and remove participants
    private readonly List<Participants> _participantsList = new List<Participants>(); 
    // Does not allow invariants -> only read
    public ReadOnlyCollection<Participants> ParticipantsList => _participantsList.AsReadOnly();

    internal AlternativeChat(string status, DateTime createdAt)
    {
        this.Id = Guid.NewGuid();
        this.Status = status;
        this.CreatedAt = createdAt;
    }

    public static AlternativeChat CreateChat(string status, DateTime createdAt, List<Participants> ptList)
    {
        AlternativeChat initiate = new AlternativeChat(status, createdAt);
        foreach (var pt in ptList)
        {
            initiate.AddParticipant(pt);
        }
        return initiate;
    }
    
    private void AddParticipant(Participants participant)
    {
        if (participant is null)
            throw new ArgumentNullException(nameof(participant));
        
        bool alreadyAdded = _participantsList.Any(p => p.UserId == participant.UserId);

        if (alreadyAdded)
            throw new ArgumentException("User already added");
        
        _participantsList.Add(participant);
    }
}