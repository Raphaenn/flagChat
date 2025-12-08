using App.Message.Commands;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace App.Message.CommandHandlers;

public class SendMessageCmdHandler : IRequestHandler<SendMessageCommand, Messages>
{
    private readonly IChatMessageRepository _chatMessageRepository; 
    
    public SendMessageCmdHandler(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }
    
    public async Task<Messages> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}