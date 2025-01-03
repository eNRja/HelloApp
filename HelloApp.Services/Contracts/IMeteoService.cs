using HelloApp.Services.Entities.Messages.Responses;
public interface IMeteoService
{
    Task<MeteoResponse> GetDegreeseByDay(string location);
}
