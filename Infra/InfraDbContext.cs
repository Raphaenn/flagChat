using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra;
public class InfraDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=admin;Password=1234;Database=flagChat;");
    }
    
    // public DbSet<Message>? Messages { get; set; }
    // public DbSet<Chats>? Chats { get; set; }
    public DbSet<Participants>? Participants { get; set; }
}