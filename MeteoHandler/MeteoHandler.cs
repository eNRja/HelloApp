using HelloApp.MeteoHandler;
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

    public async Task<Weather> MeteoRequest(string endpoint, Method method, Dictionary<string, string> request)
    {
        string fullEndpoint = _baseUrl + endpoint;
        request["key"] = _apiKey;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // JSON поля могут быть с разным регистром
        };

        var responseMeteo = await _externalApi.DataRequest(fullEndpoint, method, request);
        var serializedMeteo = JsonSerializer.Deserialize<WeatherResponse>(responseMeteo.RootElement.GetRawText(), options);

        return new Weather
        {
            City = serializedMeteo?.Location?.Name,
            Temperature = serializedMeteo?.Current?.Temp_C ?? 0
        };
    }
}
