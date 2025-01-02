using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using RestSharp;

public interface IMeteoHandler
{
    Task<WeatherResponse> GetWeather(LocationRequest request);
}
