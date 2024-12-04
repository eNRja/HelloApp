using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ExternalApi : IExternalApi
{
    private readonly RestClient _restClient;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public ExternalApi(string baseUrl, IMemoryCache cache)
    {
        _restClient = new RestClient(baseUrl);
        _cache = cache;
    }

    public async Task<string> GetDataAsync(string baseUrl, string endpoint, Dictionary<string, string> queryParams)
    {
        // Генерируем ключ для кэша
        var cacheKey = GenerateCacheKey(endpoint, queryParams);

        // Проверяем кэш
        if (_cache.TryGetValue(cacheKey, out string cachedContent))
        {
            return cachedContent;
        }

        // Формируем запрос
        var request = new RestRequest(endpoint, Method.Get);

        // Добавляем параметры запроса
        foreach (var param in queryParams)
        {
            request.AddParameter(param.Key, param.Value, ParameterType.QueryString);
        }

        var response = await _restClient.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Ошибка API: {response.StatusCode}");
        }

        var content = response.Content;

        // Кэшируем результат
        _cache.Set(cacheKey, content, _cacheDuration);

        return content;
    }

    private string GenerateCacheKey(string endpoint, Dictionary<string, string> queryParams)
    {
        var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={p.Value}"));
        return $"{endpoint}?{queryString}";
    }
}
