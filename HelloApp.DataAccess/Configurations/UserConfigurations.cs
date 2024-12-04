using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<DbUser>
{
    public void Configure(EntityTypeBuilder<DbUser> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

        builder.HasData(
            new DbUser { Id = 1, Name = "Tom", Age = 37, Email = "first@example.com" },
            new DbUser { Id = 2, Name = "Bob", Age = 41, Email = "second@example.com" },
            new DbUser { Id = 3, Name = "Sam", Age = 24, Email = "third@example.com" }
        );
    }
}
