using HelloApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

public static class UserController
{
    public static void MapUserRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/users", async (IUnitOfWork unitOfWork) =>
        {
            var users = await unitOfWork.UserRepository.GetAllAsync();
            return Results.Json(users);
        });

        endpoints.MapGet("/api/users/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id);
            return user is null ? Results.NotFound(new { message = "Пользователь не найден" }) : Results.Json(user);
        });

        endpoints.MapDelete("/api/users/{id:int}", async (int id, IUnitOfWork unitOfWork) =>
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(id);
            if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

            unitOfWork.UserRepository.Remove(user);
            await unitOfWork.SaveChangesAsync();

            return Results.Json(user);
        });

        endpoints.MapPost("/api/users", async (User user, IUnitOfWork unitOfWork) =>
        {
            await unitOfWork.UserRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return Results.Json(user);
        });

        endpoints.MapPut("/api/users", async (User userData, IUnitOfWork unitOfWork) =>
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(userData.Id);
            if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

            user.Name = userData.Name;
            user.Age = userData.Age;
            unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveChangesAsync();

            return Results.Json(user);
        });
    }
}
