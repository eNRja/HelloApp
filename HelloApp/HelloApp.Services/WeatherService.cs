using HelloApp.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class WeatherService : IWeatherService
{
    private readonly IRepository<Weather> _weatherRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExternalApiService _externalApiClient;
    private readonly string ApiKey;

    public WeatherService(IRepository<Weather> weatherRepository, IUnitOfWork unitOfWork, IExternalApiService externalApiClient, IConfiguration configuration)
    {
        _weatherRepository = weatherRepository;
        _unitOfWork = unitOfWork;
        _externalApiClient = externalApiClient;
        ApiKey = configuration["WeatherApi:ApiKey"]
             ?? throw new Exception("API Key не найден в конфигурации.");
    }

    public async Task<Weather> GetWeatherAsync()
    {
        var queryParams = new Dictionary<string, string>
        {
            { "key", ApiKey },
            { "q", "Moscow" }
        };

        var content = await _externalApiClient.GetDataAsync("https://api.weatherapi.com/v1/current.json", queryParams);

        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        var city = root.GetProperty("location").GetProperty("name").GetString();
        var temperature = root.GetProperty("current").GetProperty("temp_c").GetDouble();

        return new Weather
        {
            City = city,
            Temperature = temperature
        };
    }

    public async Task SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
}
