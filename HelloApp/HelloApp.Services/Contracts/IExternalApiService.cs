public interface IExternalApiService
{
    Task<string> GetDataAsync(string url, Dictionary<string, string> queryParams);
}
