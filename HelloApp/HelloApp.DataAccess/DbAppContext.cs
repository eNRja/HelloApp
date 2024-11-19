using HelloApp.Models;
using Microsoft.EntityFrameworkCore;

public class DbAppContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Device> Devices { get; set; } = null!;

    public DbAppContext(DbContextOptions<DbAppContext> options)
        : base(options)
    {
        // Убедимся, что база данных создается при первом обращении
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
    }

    public async Task BeginTransactionAsync()
    {
        await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
        {
            await Database.CommitTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
        {
            await Database.RollbackTransactionAsync();
        }
    }
}
