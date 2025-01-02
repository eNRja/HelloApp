using HelloApp.MeteoHandler.Entities.Messages.Responses;

public interface IMeteoService
{
    Task<WeatherResponse> GetDegreeseByDay(string location);
}
