using HelloApp.Services;

public static class DeviceController
{
    public static void MapDeviceRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/devices", async (DeviceService deviceService) =>
        {
            var devices = await deviceService.GetAllDevicesAsync();
            return Results.Json(devices);
        });

        endpoints.MapGet("/api/devices/{id:int}", async (int id, DeviceService deviceService) =>
        {
            var device = await deviceService.GetDeviceByIdAsync(id);
            return device is null ? Results.NotFound(new { message = "Устройство не найдено" }) : Results.Json(device);
        });

        endpoints.MapDelete("/api/devices/{id:int}", async (int id, DeviceService deviceService) =>
        {
            var device = await deviceService.GetDeviceByIdAsync(id);
            if (device is null) return Results.NotFound(new { message = "Устройство не найдено" });

            deviceService.RemoveDevice(device);
            await deviceService.SaveChangesAsync();

            return Results.Json(device);
        });

        endpoints.MapPost("/api/devices", async (Device device, DeviceService deviceService) =>
        {
            await deviceService.AddDeviceAsync(device);
            await deviceService.SaveChangesAsync();
            return Results.Json(device);
        });

        endpoints.MapPut("/api/devices", async (Device deviceData, DeviceService deviceService) =>
        {
            var device = await deviceService.GetDeviceByIdAsync(deviceData.Id);
            if (device is null) return Results.NotFound(new { message = "Устройство не найдено" });

            device.Name = deviceData.Name;
            device.OS = deviceData.OS;

            await deviceService.UpdateDeviceAsync(device);
            await deviceService.SaveChangesAsync();

            return Results.Json(device);
        });
    }
}
