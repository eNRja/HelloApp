public static class WeatherController
{
    public static void MapWeatherRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/weather", async (WeatherService weatherService) =>
        {
            var weather = await weatherService.GetWeatherAsync();
            return Results.Json(weather);
        });
    }
}