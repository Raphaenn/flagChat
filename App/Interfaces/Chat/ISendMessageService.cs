namespace App.Interfaces.Chat;

public interface ISendMessageService
{
    Task SendMessageAsync(Guid senderId, Guid targetId, string message, CancellationToken ct);
}