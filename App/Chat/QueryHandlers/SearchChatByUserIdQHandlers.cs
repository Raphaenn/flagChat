using App.Chat.Dto;
using App.Chat.Queries;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Chat.QueryHandlers;

public class SearchChatByUserIdQHandlers : IRequestHandler<SearchChatByUserIdQuery, ChatMessageDto>
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IParticipantRepository _participantRepository;

    public SearchChatByUserIdQHandlers(
        IChatRepository chatRepository,
        IChatMessageRepository chatMessageRepository,
        IParticipantRepository participantRepository
        )
    {
        _chatRepository = chatRepository;
        _chatMessageRepository = chatMessageRepository;
        _participantRepository = participantRepository;
    }

    public async Task<ChatMessageDto> Handle(SearchChatByUserIdQuery request, CancellationToken cancellationToken)
    {
        // search logged participant by your user id
        Participants? participant = await _participantRepository.GetParticipants(request.UserId, cancellationToken);

        if (participant == null)
        {
            throw new ArgumentException("Invalid user id");
        }

        // with participant id, find all yours open chats (limit 20)  
        IReadOnlyList<Chats>? chats = await _chatRepository.GetAllChatsByParticipantId(participant.Id, cancellationToken);

        if (chats == null)
        {
            throw new Exception("Chat not found");
        }

        ChatMessageDto response = new ChatMessageDto(participant.Id);
        
        // loading messages chats (limit 50)
        foreach (var chat in chats)
        {
            ChatDto chatDto = new ChatDto(chat.Id, participant.Id);
            IReadOnlyList<Messages> msg = await _chatMessageRepository.GetMessagesByChatId(chat.Id, 50, cancellationToken);
            foreach (var m in msg)
            {
                MessageDto parsedMsg = new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    Text = m.Content,
                    SentAt = m.SentAt
                };
                chatDto.AddMessages(parsedMsg);
            }
            response.AddChatsWithMsg(chatDto);
        }

        return response;
    }
}