using CryptoClient.Contracts;
using CryptoClient.Models;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CryptoClient.Services
{
    public class CoinCapJsonService : IJsonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CoinCapJsonService(HttpClient httpClient, string baseUrl = "https://api.coincap.io/v2/assets/")
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<CurrencyModel[]> GetFullCurrenciesInfoAsync()
        {
            var currencies = await GetTopCurrenciesAsync();
            var tasks = currencies.Select(GetModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string name)
        {
            var response = await FetchDataAsync<HistoryResponse>($"{_baseUrl}{name}/history?interval=d1");
            return FormatHistoryData(response.Data);
        }

        public async Task<Dictionary<string, double>> GetMarketsAsync(string name)
        {
            var response = await FetchDataAsync<MarketListResponse>($"{_baseUrl}{name}/markets");
            return FormatMarketsData(response.Data);
        }

        public async Task<CurrencyModel> SearchAsync(string name)
        {
            var response = await FetchDataAsync<CurrencyResponse>($"{_baseUrl}{name}/");
            return await GetModel(response.Data);
        }

        public async Task<CurrencyDTO[]> GetTopCurrenciesAsync()
        {
            var response = await FetchDataAsync<CurrencyListResponse>(_baseUrl);
            return FormatCurrenciesData(response.Data);
        }

        public async Task<Dictionary<string, double>> GetCurrenciesCoastsAsync()
        {
            var response = await FetchDataAsync<CurrencyListResponse>(_baseUrl);
            return FormatCurrenciesCoast(response.Data);
        }

        private async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<T>(url);
                return response ?? throw new Exception($"No data received from {url}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching data from {url}: {ex.Message}", ex);
            }
        }

        private static Dictionary<DateTime, double> FormatHistoryData(DayDTO[] history)
        {
            return history.ToDictionary(
                data => DateTimeOffset.FromUnixTimeMilliseconds(data.Time).DateTime,
                data => Math.Round(double.Parse(data.PriceUsd, CultureInfo.InvariantCulture), 2)
            );
        }

        private static Dictionary<string, double> FormatMarketsData(MarketDTO[] markets)
        {
            return markets
                .GroupBy(market => market.ExchangeId)
                .Select(group => group.First()) // Take the first market for each unique ExchangeId
                .OrderByDescending(market => double.Parse(market.PriceUsd, CultureInfo.InvariantCulture))
                .Take(5)
                .ToDictionary(
                    market => market.ExchangeId,
                    market => Math.Round(double.Parse(market.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        private static CurrencyDTO[] FormatCurrenciesData(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .Take(10)
                .ToArray();
        }

        private static Dictionary<string, double> FormatCurrenciesCoast(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .ToDictionary(
                    currency => currency.Id,
                    currency => Math.Round(double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        private async Task<CurrencyModel> GetModel(CurrencyDTO currencyDto)
        {
            return new CurrencyModel()
            {
                Id = currencyDto.Id,
                Symbol = currencyDto.Symbol,
                Name = currencyDto.Name,
                Price = Math.Round(double.Parse(currencyDto.PriceUsd, CultureInfo.InvariantCulture), 2),
                ChangePercent = Math.Round(double.Parse(currencyDto.ChangePercent24Hr, CultureInfo.InvariantCulture), 2),
                Link = currencyDto.Explorer,
                History = await GetHistoryAsync(currencyDto.Id),
                Markets = await GetMarketsAsync(currencyDto.Id),
            };
        }
    }
}
