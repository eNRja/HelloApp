using HelloApp.Services;
using System.Text.Json;

public class MeteoService : IMeteoService
{
    private readonly IApiHandler _apiHandler;

    public MeteoService(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<Weather> GetDegreeseByDay(string location)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "q", location }
        };

        var response = await _apiHandler.FetchDataAsync(queryParams);

        using var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        var city = root.GetProperty("location").GetProperty("name").GetString();
        var temperature = root.GetProperty("current").GetProperty("temp_c").GetDouble();

        return new Weather
        {
            City = city,
            Temperature = temperature
        };
    }
}
