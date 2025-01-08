using AutoMapper;
using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.Services.Entities.Messages.Requests;
using HelloApp.Services.Entities.Messages.Responses;

public class MeteoService : IMeteoService
{
    private readonly IMeteoHandler _apiHandler;
    private readonly IMapper _mapper;

    public MeteoService(IMeteoHandler apiHandler, IMapper mapper)
    {
        _apiHandler = apiHandler;
        _mapper = mapper;
    }

    public async Task<MeteoModel> GetDegreeseByDay(string location)
    {
        // Создаем запрос бизнес-логики (MeteoLocationRequest)
        //var businessRequest = new MeteoLocationRequest
        //{
        //    Location = location
        //};

        // Маппим бизнес-запрос в запрос для внешнего API
        //var externalRequest = _mapper.Map<LocationRequest>(businessRequest);

        // Получаем ответ от внешнего API
        var weatherResponse = await _apiHandler.GetWeather(location);

        // Маппим ответ от внешнего API в бизнес-ответ
        var meteoResponse = _mapper.Map<MeteoModel>(weatherResponse);

        return meteoResponse;
    }
}
