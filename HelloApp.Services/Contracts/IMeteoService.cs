using HelloApp.Services.Entities.Messages.Responses;
public interface IMeteoService
{
    Task<MeteoModel> GetDegreeseByDay(string location);
}
