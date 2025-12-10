namespace Domain.Entities;
public class Participants
{
    public Guid Id { get; private set; }
    public Guid UserId { get; internal set; }
    public string? Email { get; internal set; }

    protected Participants() {}
    
    private Participants(Guid userId, string email)
    {
        this.Id = Guid.NewGuid();
        this.UserId = userId;
        this.Email = email;
    }

    public static Participants CreateParticipant(Guid userId, string email)
    {
        return new Participants(userId, email);
    }
}