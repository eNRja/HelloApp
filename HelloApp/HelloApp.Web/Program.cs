var builder = WebApplication.CreateBuilder(args);

// Добавление конфигурации с помощью метода расширения
builder.AddAppConfiguration();

var app = builder.Build();

// Настройка промежуточных слоев (Middleware)
app.UseDefaultFiles();
app.UseStaticFiles();

// Настройка маршрутов
app.MapUserRoutes();
app.MapDeviceRoutes();

app.Run();
