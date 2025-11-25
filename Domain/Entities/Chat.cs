namespace Domain.Entities;
public class Chat
{
    public Guid Id { get; private set; }
    public Guid ParticipantId1 { get; private set; }
    public Guid ParticipantId2 { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Participants Participant1 { get; private set; }  // navigation
    public Participants Participant2 { get; private set; }  // navigation
    
    private Chat() { } // EF

    internal Chat(Guid participantId1, Guid participantId2, string status, DateTime createdAt)
    {
        this.Id = Guid.NewGuid();
        this.ParticipantId1 = participantId1;
        this.ParticipantId2 = participantId2;
        this.Status = status;
        this.Status = status;
        this.CreatedAt = createdAt;
    }

    public static Chat CreateChat(Guid participantId1, Guid participantId2, string status, DateTime createdAt)
    {
        // todo - validate id pt are equals 
        return new Chat(participantId1, participantId2, status, createdAt);
    }
}