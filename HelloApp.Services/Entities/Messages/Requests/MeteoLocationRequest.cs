using System.Text.Json.Serialization;

namespace HelloApp.Services.Entities.Messages.Requests
{

    public class MeteoLocationRequest
    {

        [JsonPropertyName("Name")]
        public string Location { get; set; }


    }
}