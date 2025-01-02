using HelloApp.MeteoHandler.Entities.Messages;
using HelloApp.MeteoHandler.Entities.Messages.Requests;

namespace HelloApp.MeteoHandler
{
    public class Weather
    {
        public LocationRequest Location { get; set; }
        public CurrentWeather Current { get; set; }
    }
}
