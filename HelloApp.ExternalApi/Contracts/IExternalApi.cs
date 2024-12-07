using RestSharp;
using System.Text.Json;

public interface IExternalApi
{
    Task<JsonDocument> DataRequest(string endpoint, Method method, Dictionary<string, string> request);
}
