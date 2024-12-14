namespace HelloApp.MeteoHandler
{
    public class Weather
    {
        public WeatherResponse WeatherModel { get; set; }

        public string City => WeatherModel?.Location?.Name ?? string.Empty;
        public double Temperature => WeatherModel?.Current?.Temp_C ?? 0.0;
    }

    public class WeatherResponse
    {
        public Location Location { get; set; }
        public CurrentWeather Current { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
    }

    public class CurrentWeather
    {
        public double Temp_C { get; set; }
    }
}
