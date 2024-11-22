using HelloApp.Services;

public interface IWeatherService
{
    Task<Weather> GetWeatherAsync();
}