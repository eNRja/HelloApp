using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public class ExternalApi : IExternalApi
{
    private readonly RestClient _restClient;
    private readonly string _baseUrl;

    public ExternalApi(RestClient restClient, IConfiguration configuration)
    {
        _restClient = restClient;
    }

    public async Task<JsonDocument> DataRequest(string endpoint, Method method, Dictionary<string, string> request)
    {
        var newRequest = new RestRequest(endpoint, method);

        switch (method)
        {
            case Method.Get:
                foreach (var param in request)
                {
                    newRequest.AddParameter(param.Key, param.Value, ParameterType.QueryString);
                }
                break;

            case Method.Delete:
                // DELETE метод
                break;

            default:
                throw new NotImplementedException($"Метод {method} не поддерживается.");
        }

        var response = await _restClient.ExecuteAsync(newRequest);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Ошибка API: {response.StatusCode}");
        }
        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new Exception("Ответ API пустой.");
        }

        return JsonDocument.Parse(response.Content);
    }
}
