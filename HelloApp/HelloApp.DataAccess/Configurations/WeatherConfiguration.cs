using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WeatherConfiguration : IEntityTypeConfiguration<Weather>
{
    public void Configure(EntityTypeBuilder<Weather> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.City).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Temperature).IsRequired().HasMaxLength(100);

        builder.HasData(
            new Weather { Id = 1, City = "Moscow", Temperature = 20 }
        );
    }
}