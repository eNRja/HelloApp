using HelloApp.MeteoHandler.Messages.Requests;
using HelloApp.MeteoHandler.Messages.Responses;
using RestSharp;

public interface IMeteoHandler
{
    Task<WeatherResponse> GetWeather(LocationRequest location);
}
