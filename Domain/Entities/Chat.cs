namespace Domain.Entities;
public class Chats
{
    public Guid Id { get; private set; }
    public Guid ParticipantId1 { get; private set; }
    public Guid ParticipantId2 { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Participants Participant1 { get; private set; }  // navigation
    public Participants Participant2 { get; private set; }  // navigation
    public ICollection<Messages> Messages { get; set; } = new List<Messages>();
    
    private Chats() { } // EF

    internal Chats(Guid participantId1, Guid participantId2, string status, DateTime createdAt)
    {
        this.Id = Guid.NewGuid();
        this.ParticipantId1 = participantId1;
        this.ParticipantId2 = participantId2;
        this.Status = status;
        this.Status = status;
        this.CreatedAt = createdAt;
    }

    public static Chats CreateChat(Guid participantId1, Guid participantId2, string status, DateTime createdAt)
    {
        // todo - validate id pt are equals 
        return new Chats(participantId1, participantId2, status, createdAt);
    }
}