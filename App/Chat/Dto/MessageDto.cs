namespace App.Chat.Dto;

public class MessageDto
{
    public long Id { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = default!;
    public DateTime SentAt { get; set; }
}