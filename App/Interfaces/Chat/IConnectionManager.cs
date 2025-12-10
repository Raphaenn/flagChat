namespace App.Interfaces.Chat;

public interface IConnectionManager
{
    void AddConnection(string userId, string connectionId);
    void RemoveConnection(string connectionId);
    IEnumerable<string> GetConnections(string userId);
}