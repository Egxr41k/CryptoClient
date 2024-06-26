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

namespace CryptoClient
{
    public class JsonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public async Task<List<CurrencyModel>> GetFullCurrenciesInfoAsync()
        {
            var currencies = await GetTopCurrenciesAsync();
            var result = new List<CurrencyModel>();

            var tasks = currencies.Select(async currencyDto =>
            {
                return await GetModel(currencyDto);
            });

            var completedModels = await Task.WhenAll(tasks);
            result.AddRange(completedModels);

            return result;
        }

        public JsonService(HttpClient httpClient, string baseUrl = "https://api.coincap.io/v2/assets/")
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string name)
        {
            var response = await _httpClient.GetFromJsonAsync<HistoryResponse>($"{_baseUrl}{name}/history?interval=d1");
            if (response?.Data == null) throw new Exception("No data received from history endpoint.");

            return FormatHistoryData(response.Data);
        }

        private static Dictionary<DateTime, double> FormatHistoryData(DayDTO[] history)
        {
            return history
                .ToDictionary(
                    data => DateTimeOffset.FromUnixTimeMilliseconds(data.Time).DateTime,
                    data => Math.Round(double.Parse(data.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        public async Task<Dictionary<string, double>> GetMarketsAsync(string name)
        {
            var response = await _httpClient.GetFromJsonAsync<MarketListResponse>($"{_baseUrl}{name}/markets");
            if (response?.Data == null) throw new Exception("No data received from markets endpoint.");

            return FormatMarketsData(response.Data);
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

        public async Task<CurrencyModel> SearchAsync(string name)
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyResponse>($"{_baseUrl}{name}/");

            if (response?.Data == null) throw new Exception("Object not nound");

            return await GetModel(response.Data);
        }

        public async Task<List<CurrencyDTO>> GetTopCurrenciesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyListResponse>(_baseUrl);
            if (response?.Data == null) throw new Exception("No data received from currencies endpoint.");

            return FormatCurrenciesData(response.Data);
        }

        public async Task<Dictionary<string, double>> GetCurrenciesCoasts()
        {
            var response = await _httpClient.GetFromJsonAsync<CurrencyListResponse>(_baseUrl);
            if (response?.Data == null) throw new Exception("No data received from currencies endpoint.");

            return FormatCurrenciesCoast(response.Data);
        }

        private Dictionary<string, double> FormatCurrenciesCoast(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .ToDictionary(
                    currency => currency.Id,
                    currency => Math.Round(double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        private static List<CurrencyDTO> FormatCurrenciesData(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .Take(10)
                .ToList();
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
