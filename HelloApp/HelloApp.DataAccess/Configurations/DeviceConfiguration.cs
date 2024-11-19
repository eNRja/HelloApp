using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.OS).IsRequired().HasMaxLength(50);

        builder.HasData(
            new Device { Id = 1, Name = "Xiaomi", OS = "HyperOS" },
            new Device { Id = 2, Name = "Iphone", OS = "IOS" },
            new Device { Id = 3, Name = "Laptop", OS = "Windows" },
            new Device { Id = 4, Name = "Huawei", OS = "Android" }
        );
    }
}
