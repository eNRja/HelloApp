public interface IExternalApi
{
    Task<string> GetDataAsync(string baseUrl, string endpoint, Dictionary<string, string> queryParams);
}
