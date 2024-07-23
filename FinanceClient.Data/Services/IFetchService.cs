namespace FinanceClient.Data.Services
{
    public interface IFetchService
    {
        Task<T> FetchDataAsync<T>(string url);
    }
}
