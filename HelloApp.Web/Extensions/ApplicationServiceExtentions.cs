﻿using HelloApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            return new RestClient(baseUrl);  // Создаём RestClient с конфигурацией
        });

        // Регистрация ExternalApi
        builder.Services.AddScoped<ExternalApi>(); // Регистрация через интерфейс

        // Регистрация MeteoHandler (он будет использовать ExternalApi через наследование)
        builder.Services.AddScoped<IMeteoHandler, MeteoHandler>();
        builder.Services.AddScoped<IMeteoService, MeteoService>();

        // Настройка AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddAutoMapper(typeof(MappingMeteoProfile));

        // Чтение конфигурации из файла appsettings.Secrets.json
        builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

        // Настройка контроллеров с представлениями
        builder.Services.AddControllersWithViews();
    }
}
