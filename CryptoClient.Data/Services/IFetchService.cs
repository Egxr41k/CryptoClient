namespace CryptoClient.Data.Services
{
    public interface IFetchService
    {
        Task<T> FetchDataAsync<T>(string url);
    }
}
