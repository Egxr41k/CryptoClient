using CryptoClient.Contracts;
using CryptoClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoClient.Services
{
    internal class NbuJsonService : IJsonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public NbuJsonService(HttpClient httpClient, string baseUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/")
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<List<NbuCurrencyDTO>> GetTopCurrenciesAsync()
        {
            var response = await FetchDataAsync<NbuCurrencyDTO[]>($"{_baseUrl}exchange?json");
            return FormatCurrenciesData(response);
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string currencyCode)
        {
            var tasks = new List<Task<NbuCurrencyDTO>>();
            for (var date = DateTime.UtcNow.AddMonths(-1); date <= DateTime.Now; date = date.AddDays(1))
            {
                var formattedDate = date.ToString("yyyyMMdd");
                tasks.Add(FetchDataAsync<NbuCurrencyDTO>($"{_baseUrl}exchange?valcode={currencyCode}&date={formattedDate}&json"));
            }

            var histories = await Task.WhenAll(tasks);
            return FormatHistoryData(histories);
        }

        private async Task<T> FetchDataAsync<T>(string url) where T : class
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<T>(url);

                Console.WriteLine("data deserelized succesfully:" + data.ToString());

                return data ?? throw new Exception($"No data received from {url}");
            }
            catch (JsonException ex)
            {
                var data = await _httpClient.GetFromJsonAsync<T[]>(url);
                return data.First() ?? throw new Exception($"No data received from {url}");
            }
            catch (HttpRequestException ex)
            {
                // Log error as needed
                throw new Exception($"Error fetching data from {url}: {ex.Message}", ex);
            }
        }

        private static List<NbuCurrencyDTO> FormatCurrenciesData(NbuCurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => currency.Rate)
                .Take(5)
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
            return new CurrencyModel()
            {
                Id = currencyDto.R030.ToString(),
                Symbol = currencyDto.Cc,
                Name = currencyDto.Txt,
                Price = currencyDto.Rate,
                ChangePercent = 0.0,
                Link = "",
                History = await GetHistoryAsync(currencyDto.Cc),
            };
        }
    }
}