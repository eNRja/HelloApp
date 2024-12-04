using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class DeviceConfiguration : IEntityTypeConfiguration<DbDevice>
{
    public void Configure(EntityTypeBuilder<DbDevice> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.OS).IsRequired().HasMaxLength(50);

        builder.HasData(
            new DbDevice { Id = 1, Name = "Xiaomi", OS = "HyperOS" },
            new DbDevice { Id = 2, Name = "Iphone", OS = "IOS" },
            new DbDevice { Id = 3, Name = "Laptop", OS = "Windows" },
            new DbDevice { Id = 4, Name = "Huawei", OS = "Android" }
        );
    }
}
