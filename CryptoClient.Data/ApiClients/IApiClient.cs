using CryptoClient.Data.Models;

namespace CryptoClient.Data.Services
{
    public interface IApiClient
    {
        Task<Dictionary<string, double>> GetCurrenciesCoastsAsync();
        Task<CurrencyModel[]> GetFullCurrenciesInfoAsync();
        Task<Dictionary<DateTime, double>> GetHistoryAsync(string currencyCode);
        Task<Dictionary<string, double>> GetMarketsAsync(string currencyCode);
        Task<CurrencyModel> SearchAsync(string currencyCode);
    }
}