using Domain.Entities;

namespace App.Abstractions;

public interface IChatRepository
{
    Task CreateChat(Chats data);
    Task<Chats> GetChatById();
    Task<Chats> GetChatUserId();
    Task DeleteChat();
}