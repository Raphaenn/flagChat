namespace Domain.Entities;
public class Messages
{
    public Guid Id { get; private set; }
    public Guid ChatId { get; private set; }
    public Guid ParticipantId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }   
    public DateTime UpdatedAt { get; private set; }
    
    public Participants Participant { get; private set; }
    public Chats Chat { get; private set; }
    
    private Messages(Guid chatId, Guid participantId, string content, DateTime createdAt)
    {
        this.Id = Guid.NewGuid();
        this.ChatId = chatId;
        this.ParticipantId = participantId;
        this.Content = content;
        this.CreatedAt = createdAt;
    }

    public static Messages CreateMessage(Guid chatId, Guid userId, string content, DateTime createdAt)
    {
        return new Messages(chatId, userId, content, createdAt);
    }
}