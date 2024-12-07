using HelloApp.MeteoHandler;
using RestSharp;
using System.Text.Json;

public interface IMeteoHandler
{
    Task<Weather> MeteoRequest(string endpoint, Method method, Dictionary<string, string> request);
}
