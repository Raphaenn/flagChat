namespace App.Chat.Dto;

public class ChatMessageDto
{
    public Guid ParticipantId { get; set; }
    
    private readonly List<ChatDto> _chatsList = new List<ChatDto>();
    public IReadOnlyCollection<ChatDto> ChatsList => _chatsList.AsReadOnly();

    public ChatMessageDto(Guid participantId)
    {
        ParticipantId = participantId;
    }

    public void AddChatsWithMsg(ChatDto chat)
    {
        _chatsList.Add(chat);
    }
}