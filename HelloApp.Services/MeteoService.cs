using AutoMapper;
using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using HelloApp.Services.Entities;
using HelloApp.Services.Entities.Messages;

public class MeteoService : IMeteoService
{
    private readonly IMeteoHandler _apiHandler;
    private readonly IMapper _mapper;

    public MeteoService(IMeteoHandler apiHandler, IMapper mapper)
    {
        _apiHandler = apiHandler;
        _mapper = mapper;
    }

    public async Task<MeteoModel> GetDegreeseByDay(MeteoFilter filter)
    {
        // Сформировать запрос для внешнего API
        var externalRequest = new LocationRequest
        {
            Location = filter.Location
        };

        // Получить ответ от внешнего API
        var weatherResponse = await _apiHandler.GetWeather(externalRequest);

        // Маппинг ответа API в бизнес-модель
        var meteoModel = _mapper.Map<MeteoModel>(weatherResponse);

        return meteoModel;
    }
}
