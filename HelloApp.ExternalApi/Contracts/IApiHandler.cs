public interface IApiHandler
{
    Task<string> FetchDataAsync(Dictionary<string, string> queryParams);
}
