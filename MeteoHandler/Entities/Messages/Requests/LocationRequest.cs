using System.Text.Json.Serialization;

namespace HelloApp.MeteoHandler.Entities.Messages.Requests
{
    public class LocationRequest
    {
        [JsonPropertyName("Name")]
        public string? Location { get; set; }
    }
}
