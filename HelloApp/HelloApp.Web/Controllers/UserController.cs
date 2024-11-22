using HelloApp.DataAccess;

public static class UserController
{
    public static void MapUserRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/users", async (UserService userService, DeviceService deviceService) =>
        {
            var users = await userService.GetAllUsersAsync();

            foreach (var user in users)
            {
                if (user.DeviceId.HasValue)
                {
                    user.Device = await deviceService.GetDeviceByIdAsync(user.DeviceId.Value);
                }
            }

            return Results.Json(users);
        });

        endpoints.MapGet("/api/users/{id:int}", async (int id, UserService userService, DeviceService deviceService) =>
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user is null)
                return Results.NotFound(new { message = "Пользователь не найден" });

            // Populate the Device property using deviceService
            if (user.DeviceId.HasValue)
            {
                user.Device = await deviceService.GetDeviceByIdAsync(user.DeviceId.Value);
            }

            return user is null ? Results.NotFound(new { message = "Пользователь не найден" }) : Results.Json(user);
        });

        endpoints.MapDelete("/api/users/{id:int}", async (int id, UserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

            userService.RemoveUser(user);
            await userService.SaveChangesAsync();

            return Results.Json(user);
        });

        endpoints.MapPost("/api/users", async (User user, UserService userService) =>
        {
            try
            {
                await userService.AddUserAsync(user);
                await userService.SaveChangesAsync();
                return Results.Json(user);
            }
            catch (InvalidOperationException ex)
            {
                // Возвращаем ошибку, если пользователь с таким email уже существует
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        endpoints.MapPut("/api/users", async (User userData, UserService userService, DeviceService deviceService) =>
        {
            var user = await userService.GetUserByIdAsync(userData.Id);
            if (user is null) return Results.NotFound(new { message = "Пользователь не найден" });

            user.Name = userData.Name;
            user.Age = userData.Age;
            user.Email = userData.Email;
            user.DeviceId = userData.DeviceId;

            if (user.DeviceId.HasValue)
            {
                var device = await deviceService.GetDeviceByIdAsync(user.DeviceId.Value);
                if (device != null)
                {
                    user.Device = device;  // Присваиваем актуальное устройство в свойство Device
                }
            }

            await userService.UpdateUserAsync(user);
            await userService.SaveChangesAsync();

            return Results.Json(user);
        });
    }
}
