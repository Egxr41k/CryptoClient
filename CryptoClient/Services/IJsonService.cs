using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoClient.Services
{
    internal interface IJsonService
    {
        Task<Dictionary<string, double>> GetCurrenciesCoastsAsync();
        Task<CurrencyModel[]> GetFullCurrenciesInfoAsync();
        Task<Dictionary<DateTime, double>> GetHistoryAsync(string currencyCode);
        Task<Dictionary<string, double>> GetMarketsAsync(string currencyCode);
        Task<CurrencyModel> SearchAsync(string currencyCode);
    }
}