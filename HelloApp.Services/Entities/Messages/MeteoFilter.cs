using System.Text.Json.Serialization;

namespace HelloApp.Services.Entities.Messages
{

    public class MeteoFilter
    {

        //[JsonPropertyName("Name")]
        public string Location { get; set; }


    }
}