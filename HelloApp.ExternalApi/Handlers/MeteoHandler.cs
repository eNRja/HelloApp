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

    public async Task<JsonDocument> MeteoRequest(string endpoint, Method method, Dictionary<string, string> request)
    {
        string fullEndpoint = _baseUrl + endpoint;
        request["key"] = _apiKey;

        return await _externalApi.DataRequest(fullEndpoint, method, request );
    }
}
