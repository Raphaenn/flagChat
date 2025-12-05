namespace Domain.Entities;
public class Messages
{
    public Guid Id { get; private set; }
    public Guid ChatId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; }
    public DateTime SentAt { get; private set; }

    public Participants? Participant { get; private set; }
    public Chats? Chat { get; private set; }
    
    private Messages(Guid chatId, Guid participantId, string content, DateTime sentAt)
    {
        this.Id = Guid.NewGuid();
        this.ChatId = chatId;
        this.SenderId = participantId;
        this.Content = content;
        this.SentAt = sentAt;
    }

    public static Messages CreateMessage(Guid chatId, Guid userId, string content, DateTime createdAt)
    {
        return new Messages(chatId, userId, content, createdAt);
    }
}