using HelloApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

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
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Регистрация RestClient
        builder.Services.AddScoped<RestClient>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var baseUrl = configuration["ExternalApi:WeatherUrl"];
            return new RestClient(baseUrl);
        });

        // Регистрация ExternalApi
        builder.Services.AddScoped<IExternalApi>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var cache = provider.GetRequiredService<IMemoryCache>();
            var baseUrl = configuration["ExternalApi:WeatherUrl"];
            return new ExternalApi(baseUrl, cache);
        });

        // Регистрация стратегий и фасада
        builder.Services.AddScoped<IApiHandler, CurrentWeatherHandler>();
        builder.Services.AddScoped<IMeteoService, MeteoService>();

        // Настройка AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        // Чтение конфигурации из файла appsettings.Secrets.json
        builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

        // Настройка контроллеров с представлениями
        builder.Services.AddControllersWithViews();
    }
}
