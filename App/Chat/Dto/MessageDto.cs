namespace App.Chat.Dto;

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = default!;
    public DateTime SentAt { get; set; }
}