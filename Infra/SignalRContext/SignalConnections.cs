namespace Infra.SignalRContext;

public interface IConnectionManager
{
    void AddConnection(string userId, string connectionId);
    void RemoveConnection(string connectionId);
    IEnumerable<string> GetConnections(string userId);
}

public class SignalConnections : IConnectionManager
{
    private readonly Dictionary<string, HashSet<string>> _connections = new();

    public void AddConnection(string userId, string connectionId)
    {
        // O uso do lock no método AddConnection é para garantir segurança em multithreading.
        lock (_connections)
        {
            if (!_connections.TryGetValue(userId, out var connectionIds))
            {
                connectionIds = new HashSet<string>();
                _connections[userId] = connectionIds;
            }
            connectionIds.Add(connectionId);
        }
    }

    public void RemoveConnection(string connectionId)
    {
        lock (_connections)
        {
            foreach (var connections in _connections.Values)
            {
                connections.Remove(connectionId);
            }
        }
    }

    public IEnumerable<string> GetConnections(string userId)
    {
        if (_connections.TryGetValue(userId, out var connectionIds))
        {
            return connectionIds;
        }
        return Enumerable.Empty<string>();
    }
}