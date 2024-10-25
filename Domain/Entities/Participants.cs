namespace Domain.Entities;
public class Participants
{
    public string? Id { get; private set; }
    public string? UserId { get; private set; }
    public string? Name { get; private set; }

    public Participants(string userId, string name)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        Name = name;
    }
}