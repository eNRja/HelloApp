using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using RestSharp;
using System.Text.Json;

public class MeteoService : IMeteoService
{
    private readonly IMeteoHandler _apiHandler;

    public MeteoService(IMeteoHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<WeatherResponse> GetDegreeseByDay(string location)
    {
        var request = new LocationRequest();
        request.Name = location;

        return await _apiHandler.GetWeather(request);
    }
}
