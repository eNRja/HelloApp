using HelloApp.Models;
using Microsoft.EntityFrameworkCore;

public class DbAppContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        //public DbSet<Order> Orders { get; set; } = null!;

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
            // Убедимся, что база данных создается при первом обращении
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Инициализируем данные для Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 37 },
                new User { Id = 2, Name = "Bob", Age = 41 },
                new User { Id = 3, Name = "Sam", Age = 24 }
            );

            // Дополнительная настройка сущностей может быть добавлена здесь
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
