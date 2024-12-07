using Azure.Core;
using HelloApp.Services;
using RestSharp;
using System.Text.Json;

public class MeteoService : IMeteoService
{
    private readonly IMeteoHandler _apiHandler;

    public MeteoService(IMeteoHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<Weather> GetDegreeseByDay(string location, string endpoint)
    {
        var request = new Dictionary<string, string>
        {
            { "q", location }
        };
        
        var response = await _apiHandler.MeteoRequest(endpoint, Method.Get, request);

        var root = response.RootElement;

        var city = root.GetProperty("location").GetProperty("name").GetString();
        var temperature = root.GetProperty("current").GetProperty("temp_c").GetDouble();

        return new Weather
        {
            City = city,
            Temperature = temperature
        };
    }
}
