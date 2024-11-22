using HelloApp.DataAccess;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

public class WeatherService
{
    private readonly IRepository<Weather> _weatherRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HttpClient _httpClient;
    private const string WeatherApiUrl = "https://api.weatherapi.com/v1/current.json";
    private readonly string ApiKey;
    //private const string ApiKey = "01362c128b934ef0b64103219242111";

    public WeatherService(IRepository<Weather> weatherRepository, IUnitOfWork unitOfWork, HttpClient httpClient, IConfiguration configuration)
    {
        _weatherRepository = weatherRepository;
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
        ApiKey = configuration["WeatherApi:ApiKey"]
             ?? throw new Exception("API Key не найден в конфигурации.");
    }

    public async Task<Weather> GetWeatherAsync()
    {

        var url = $"{WeatherApiUrl}?key={ApiKey}&q=Moscow";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Не удалось получить данные о погоде для города Moscow");

        var content = await response.Content.ReadAsStringAsync();

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
