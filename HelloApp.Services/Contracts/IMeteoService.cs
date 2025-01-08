using HelloApp.Services.Models;
public interface IMeteoService
{
    Task<MeteoModel> GetDegreeseByDay(MeteoFilter filter);
}
