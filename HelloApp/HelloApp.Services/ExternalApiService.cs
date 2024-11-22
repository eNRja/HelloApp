using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public ExternalApiService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<string> GetDataAsync(string url, Dictionary<string, string> queryParams)
    {
        // Генерируем ключ для кэша на основе URL и параметров запроса
        var cacheKey = GenerateCacheKey(url, queryParams);

        // Попытка получить данные из кэша
        if (_cache.TryGetValue(cacheKey, out string cachedContent))
        {
            return cachedContent;
        }

        // Если данных нет в кэше, делаем запрос к API
        var uriBuilder = new UriBuilder(url);
        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
        foreach (var param in queryParams)
        {
            query[param.Key] = param.Value;
        }
        uriBuilder.Query = query.ToString();

        var response = await _httpClient.GetAsync(uriBuilder.ToString());

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Не удалось получить данные от API. Статус: {response.StatusCode}");
        }

        var content = await response.Content.ReadAsStringAsync();

        // Кэшируем ответ
        _cache.Set(cacheKey, content, _cacheDuration);

        return content;
    }

    // Метод для генерации уникального ключа кэша на основе URL и параметров запроса
    private string GenerateCacheKey(string url, Dictionary<string, string> queryParams)
    {
        var queryString = string.Join("&", queryParams.Select(param => $"{param.Key}={param.Value}"));
        return $"{url}?{queryString}";
    }
}
