public static class WeatherController
{
    public static void MapWeatherRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/weather", async (IMeteoService meteoService, string location = "Moscow") =>
        {
            var weather = await meteoService.GetDegreeseByDay(location, "/current.json");
            return Results.Json(weather);
        });
    }
}
