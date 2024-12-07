using RestSharp;
using System.Text.Json;

public interface IMeteoHandler
{
    Task<JsonDocument> MeteoRequest(string endpoint, Method method, Dictionary<string, string> request);
}
