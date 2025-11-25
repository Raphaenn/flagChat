using Domain.Entities;

namespace App.Abstractions;

public interface IChatRepository
{
    Task CreateChat(Chat data);
    Task<Chat> GetChatById();
    Task<Chat> GetChatUserId(string userId);
    Task DeleteChat();
}