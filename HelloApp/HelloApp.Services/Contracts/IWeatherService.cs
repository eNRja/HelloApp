using HelloApp.Services;

public interface IWeatherService
{
    Task<Weather> GetWeatherAsync();
    Task SaveChangesAsync();
}