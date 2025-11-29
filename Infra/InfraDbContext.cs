using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra;
public class InfraDbContext : DbContext
{
    public InfraDbContext(DbContextOptions<InfraDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Só configura se ainda não tiver sido configurado (runtime usa AddDbContext)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=admin;Password=1234;Database=flagChat;");
        }
    }
    
    public DbSet<Chats>? Chats { get; set; }
    public DbSet<Messages> Message { get; set; }
    public DbSet<Participants>? Participants { get; set; }

    # region Required
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Participants>(participant =>
        {
            participant.ToTable("participants");

            participant.HasKey(p => p.Id);

            participant
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(300);
        });

        builder.Entity<Chats>(chat =>
        {
            chat.ToTable("chats");
            chat.HasKey(c => c.Id);
            chat.Property(c => c.CreatedAt)
                .IsRequired();

            chat.Property(c => c.ParticipantId1)
                .IsRequired();
            
            chat.Property(c => c.ParticipantId2)
                .IsRequired();

            chat.HasOne(c => c.Participant1)
                .WithMany()
                .HasForeignKey(c => c.ParticipantId1)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Chats_Participants_ParticipantId1");
            
            chat.HasOne(c => c.Participant2)
                .WithMany()
                .HasForeignKey(c => c.ParticipantId1)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Chats_Participants_ParticipantId2");
        });

        builder.Entity<Messages>(message =>
        {
            message.ToTable("messages");

            message.HasKey(m => m.Id);
            message.Property(m => m.ChatId).IsRequired();
            message.Property(m => m.ParticipantId).IsRequired();

            message.HasOne(m => m.Chat)
                .WithMany(c => c.MessagesList)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Messages_Chats_ChatId");
            
            message.HasOne(m => m.Participant)
                .WithMany()
                .HasForeignKey(m => m.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Messages_Participants_ParticipantId");
        });

    }
    #endregion
}   