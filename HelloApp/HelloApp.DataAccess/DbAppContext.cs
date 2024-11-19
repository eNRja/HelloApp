using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;

public class DbAppContext : DbContext
{
    public DbSet<HelloApp.DataAccess.User> Users { get; set; }
    public DbSet<HelloApp.DataAccess.Device> Devices { get; set; }


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
