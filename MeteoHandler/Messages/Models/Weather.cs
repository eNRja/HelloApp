using HelloApp.MeteoHandler.Messages.Requests;

namespace HelloApp.MeteoHandler.Messages.Models
{
    public class Weather
    {
        public LocationRequest Location { get; set; }
        public CurrentWeather Current { get; set; }
    }
}
