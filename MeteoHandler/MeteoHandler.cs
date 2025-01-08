using HelloApp.MeteoHandler;
using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public class MeteoHandler : IMeteoHandler
{
    private readonly IExternalApi _externalApi;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public MeteoHandler(IExternalApi externalApi, IConfiguration configuration)
    {
        _externalApi = externalApi;

        _baseUrl = configuration["ExternalApi:WeatherUrl"]
                   ?? throw new Exception("Base URL для внешнего API не найден в конфигурации.");

        _apiKey = configuration["WeatherApi:ApiKey"]
                  ?? throw new Exception("API Key для WeatherApi не найден в конфигурации.");
    }

    public async Task<WeatherResponse> GetWeather(LocationRequest location)
    {
        const string endpoint = "/current.json";
        var fullEndpoint = _baseUrl + endpoint;
        var options = new JsonSerializerOptions();
        var apiRequest = new Dictionary<string, string>();

        options.PropertyNameCaseInsensitive = true; // JSON поля могут быть с разным регистром
        apiRequest.Add("key", _apiKey);
        apiRequest.Add("q", location.Location);

        var responseMeteo = await _externalApi.DataRequest(fullEndpoint, Method.Get, apiRequest);
        var serializedMeteo = JsonSerializer.Deserialize<Weather>(responseMeteo.RootElement.GetRawText(), options);
        var result = new WeatherResponse()
        {
            Weather = serializedMeteo
        };

        return result;
    }
}
