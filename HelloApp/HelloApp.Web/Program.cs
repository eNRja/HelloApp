using HelloApp.Models;
using HelloApp.Data;
using HelloApp.Data.Repositories;
using HelloApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов
builder.Services.AddDbContext<DbAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<UserService>();  // Регистрируем сервис UserService
builder.Services.AddScoped<DeviceService>();  // Регистрируем сервис для девайсов
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapUserRoutes();
app.MapDeviceRoutes();

app.Run();
