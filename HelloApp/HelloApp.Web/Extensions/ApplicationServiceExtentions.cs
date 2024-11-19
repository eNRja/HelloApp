using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore;
public static class ApplicationServiceExtensions
{
    public static void AddAppConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DbAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<DeviceService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddControllersWithViews();
    }
}
