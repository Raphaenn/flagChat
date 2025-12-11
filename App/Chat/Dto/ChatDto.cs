using System.Collections.ObjectModel;

namespace App.Chat.Dto;

public class ChatDto
{
    public Guid ChatId { get; set; }
    public Guid ParticipantId { get; set; }

    private readonly List<MessageDto> _messageList = new List<MessageDto>();
    public ReadOnlyCollection<MessageDto> MessageList => _messageList.AsReadOnly();

    public ChatDto(Guid chatId, Guid participantId)
    {
        ChatId = chatId;
        ParticipantId = participantId;
    }

    public void AddMessages(MessageDto msg)
    {
        _messageList.Add(msg);
    }
};