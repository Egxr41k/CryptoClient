using CryptoClient.Data.Contracts;
using CryptoClient.Logging;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient.Data.Services
{
    public class NbuClient : IApiClient
    {
        private readonly IFetchService _fetchService;
        private readonly string _baseUrl;

        public NbuClient(IFetchService fetchService, string baseUrl = "https://bank.gov.ua/NBU_Exchange/exchange_site")
        {
            _fetchService = fetchService;
            _baseUrl = baseUrl;
        }

        public async Task<List<NbuCurrencyDTO>> GetTopCurrenciesAsync()
        {
            var response = await _fetchService.FetchDataAsync<NbuCurrencyDTO[]>($"{_baseUrl}?json");
            return FormatCurrenciesData(response);
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string currencyCode)
        {
            var start = DateTime.UtcNow.AddYears(-1).ToString("yyyyMMdd");
            var end = DateTime.UtcNow.ToString("yyyyMMdd");
            var url = $"{_baseUrl}?start={start}&end={end}&valcode={currencyCode}&sort=exchangedate&order=desc&json";
            var response = await _fetchService.FetchDataAsync<NbuCurrencyDTO[]>(url);

            return FormatHistoryData(response);
        }

        private static List<NbuCurrencyDTO> FormatCurrenciesData(NbuCurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => currency.Rate)
                .Take(10)
                .ToList();
        }

        private static Dictionary<DateTime, double> FormatHistoryData(NbuCurrencyDTO[] history)
        {
            return history.ToDictionary(
                data => DateTime.ParseExact(data.Exchangedate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                data => Math.Round(data.Rate, 2)
            );
        }

        public async Task<Dictionary<string, double>> GetCurrenciesCoastsAsync()
        {
            return await Task.FromResult(new Dictionary<string, double>());
        }

        public async Task<CurrencyModel[]> GetFullCurrenciesInfoAsync()
        {
            var currencies = await GetTopCurrenciesAsync();
            var tasks = currencies.Select(GetModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<Dictionary<string, double>> GetMarketsAsync(string currencyCode)
        {
            return await Task.FromResult(new Dictionary<string, double>());
        }

        public async Task<CurrencyModel> SearchAsync(string currencyCode)
        {
            return await Task.FromResult(new CurrencyModel());
        }

        private async Task<CurrencyModel> GetModel(NbuCurrencyDTO currencyDto)
        {
            var history = await GetHistoryAsync(currencyDto.Cc);
            var yesterday = DateTime.Today.AddDays(-1);
            var changePercent = history[DateTime.Today] / history[yesterday];

            return new CurrencyModel()
            {
                Id = currencyDto.R030.ToString(),
                Symbol = currencyDto.Cc,
                Name = currencyDto.Txt,
                Price = currencyDto.Rate,
                Link = "",
                History = history,
                ChangePercent = Math.Round(changePercent, 2)
            };
        }
    }
}