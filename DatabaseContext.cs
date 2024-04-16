using ConsumerApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumerApplication.Data;

public class DatabaseContext : DbContext
{
    protected readonly IConfiguration _config;

    public DatabaseContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityColumns();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_config.GetConnectionString("Default"));
    }
    
    public DbSet<users> users { get; set; }
}