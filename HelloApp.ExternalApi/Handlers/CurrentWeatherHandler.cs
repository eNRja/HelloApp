using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CurrentWeatherHandler : IApiHandler
{
    private readonly IExternalApi _externalApi;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    public CurrentWeatherHandler(IExternalApi externalApi, IConfiguration configuration)
    {
        _externalApi = externalApi;

        _baseUrl = configuration["ExternalApi:WeatherUrl"]
                   ?? throw new Exception("Base URL для внешнего API не найден в конфигурации.");

        _apiKey = configuration["WeatherApi:ApiKey"]
                  ?? throw new Exception("API Key для WeatherApi не найден в конфигурации.");
    }

    public async Task<string> FetchDataAsync(Dictionary<string, string> queryParams)
    {
        queryParams["key"] = _apiKey;

        return await _externalApi.GetDataAsync(_baseUrl, "/current.json", queryParams);
    }
}
