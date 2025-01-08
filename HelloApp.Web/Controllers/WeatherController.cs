using HelloApp.Services.Models;

public static class WeatherController
{
    public static void MapWeatherRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/weather", async (IMeteoService meteoService, string location = "Moscow") =>
        {
            var filter = new MeteoFilter { Location = "Moscow" };
            var weather = await meteoService.GetDegreeseByDay(filter);
            return Results.Json(weather);
        });
    }
}
