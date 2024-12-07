using HelloApp.MeteoHandler;

public interface IMeteoService
{
    Task<Weather> GetDegreeseByDay(string location, string endpoint);
}
