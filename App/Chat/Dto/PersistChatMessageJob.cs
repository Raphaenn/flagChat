namespace App.Chat.Dto;

public record PersistChatMessageJob(Guid ChatId,
    Guid SenderId,
    string Content,
    DateTime CreatedAtUtc);