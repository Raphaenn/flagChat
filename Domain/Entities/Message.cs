namespace Domain.Entities;
public class Messages
{
    public string Id { get; private set; }
    public string ChatId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public string Timestamp { get; set; }   
    
    public Messages(string chatId, string userId, string content, string timestamp)
    {
        this.Id = Guid.NewGuid().ToString();
        this.ChatId = chatId;
        this.UserId = userId;
        this.Content = content;
        this.Timestamp = timestamp;
    }
}