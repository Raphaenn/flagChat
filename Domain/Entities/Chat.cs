namespace Domain.Entities;
public class Chats
{
    public string Id { get; private set; }
    public List<string> Participants { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Chats(List<string> participants, DateTime createdAt)
    {
        Id = Guid.NewGuid().ToString();
        this.Participants = participants;
        this.CreatedAt = createdAt;
    }
}