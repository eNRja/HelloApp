using HelloApp.Services.Entities.Messages;
public interface IMeteoService
{
    Task<MeteoModel> GetDegreeseByDay(MeteoFilter filter);
}
