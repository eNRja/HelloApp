using System.Text.Json.Serialization;

namespace HelloApp.MeteoHandler.Entities.Messages.Responses

{
    public class WeatherResponse
    {
        [JsonIgnore]
        public Weather Weather { get; set; }
        public string City => Weather?.Location?.Name ?? string.Empty;
        public double Temperature => Weather?.Current?.Temp_C ?? 0.0;
    }
}
