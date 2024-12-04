using HelloApp.Services;

public interface IMeteoService
{
    Task<Weather> GetDegreeseByDay(string location);
}
