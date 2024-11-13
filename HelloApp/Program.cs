using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Настраиваем подключение к базе данных
string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

// Регистрируем обобщённые репозитории и UnitOfWork для Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Настройка маршрутов API
app.MapGet("/api/users", async (IUnitOfWork unitOfWork) => await unitOfWork.UserRepository.GetAllAsync());

app.MapGet("/api/users/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
{
    var user = await unitOfWork.UserRepository.GetByIdAsync(id);
    return user is null ? Results.NotFound(new { message = "Пользователь не найден" }) : Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
{
    var user = await unitOfWork.UserRepository.GetByIdAsync(id);
    if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

    unitOfWork.UserRepository.Remove(user);
    await unitOfWork.SaveChangesAsync();

    return Results.Json(user);
});

app.MapPost("/api/users", async (User user, IUnitOfWork unitOfWork) =>
{
    await unitOfWork.UserRepository.AddAsync(user);
    await unitOfWork.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPut("/api/users", async (User userData, IUnitOfWork unitOfWork) =>
{
    var user = await unitOfWork.UserRepository.GetByIdAsync(userData.Id);
    if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

    user.Name = userData.Name;
    user.Age = userData.Age;
    unitOfWork.UserRepository.Update(user);
    await unitOfWork.SaveChangesAsync();

    return Results.Json(user);
});

app.Run();
