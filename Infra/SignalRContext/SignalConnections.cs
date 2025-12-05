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
            string? userToRemove = null;

            foreach (var kvp in _connections)
            {
                if (kvp.Value.Remove(connectionId))
                {
                    if (kvp.Value.Count == 0)
                        userToRemove = kvp.Key;

                    break;
                }
            }

            if (userToRemove is not null)
                _connections.Remove(userToRemove);
        }
    }

    public IEnumerable<string> GetConnections(string userId)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(userId, out var connectionIds))
                return connectionIds.ToList(); // <-- evita expor referência interna
            
            return Enumerable.Empty<string>();
        }
    }
}
