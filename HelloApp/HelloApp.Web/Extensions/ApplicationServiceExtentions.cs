using HelloApp.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
public static class ApplicationServiceExtensions
{
    public static void AddAppConfiguration(this WebApplicationBuilder builder)
    {
        // Настройка DbContext
        builder.Services.AddDbContext<DbAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Регистрация репозиториев
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Регистрация сервисов
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<DeviceService>();
        // TODO: узнать почему нельзя удалить строку ниже, не понимаю в чем разница <WeatherService> && <IWeatherService, WeatherService>
        builder.Services.AddScoped<WeatherService>();
        builder.Services.AddScoped<IExternalApiService, ExternalApiService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

        // Настройка контроллеров с представлениями
        builder.Services.AddControllersWithViews();

        // Регистрация HttpClient (для WeatherService)
        builder.Services.AddHttpClient();
    }
}
