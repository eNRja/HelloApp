using Azure.Core;
using HelloApp.MeteoHandler;
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

        return await _apiHandler.MeteoRequest(endpoint, Method.Get, request);
    }
}
