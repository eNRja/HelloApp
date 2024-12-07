namespace HelloApp.MeteoHandler

{
    public class Weather
    {
        public string City { get; set; } = "";
        public double Temperature { get; set; }
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