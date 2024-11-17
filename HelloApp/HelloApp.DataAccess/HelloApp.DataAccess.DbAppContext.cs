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
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Tom", Age = 37, Email = "first@example.com" },
            new User { Id = 2, Name = "Bob", Age = 41, Email = "second@example.com" },
            new User { Id = 3, Name = "Sam", Age = 24, Email = "third@example.com" }
        );
        modelBuilder.Entity<Device>().HasData(
            new Device { Id = 1, Name = "Xiaomi", OS = "HyperOS", },
            new Device { Id = 2, Name = "Iphone", OS = "IOS", },
            new Device { Id = 3, Name = "Laptop", OS = "Windows", },
            new Device { Id = 4, Name = "Huawei", OS = "Android", }
        );

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
